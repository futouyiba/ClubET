//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2020年10月21日 15:15:48
//------------------------------------------------------------

using System.Collections.Generic;
using System.Threading;
using ETModel;
using ETModel.NKGMOBA.Battle.Fsm;
using ETModel.NKGMOBA.Battle.State;
using UnityEngine;

namespace ETHotfix
{
    [Event(EventIdType.CancelAttack)]
    public class CancelAttackEvent: AEvent<long>
    {
        public override void Run(long a)
        {
            UnitComponent.Instance.Get(a).GetComponent<CommonAttackComponent>().CancelCommonAttackExtension();
        }
    }

    [Event(EventIdType.CancelAttackWithOutResetAttackTarget)]
    public class CancelAttackWithOutResetAttackTargetEvent: AEvent<long>
    {
        public override void Run(long a)
        {
            UnitComponent.Instance.Get(a).GetComponent<CommonAttackComponent>().CancelCommonAttackWithOutResetTarget();
        }
    }

    [ObjectSystem]
    public class CommonAttackComponentUpdateSystem: UpdateSystem<CommonAttackComponent>
    {
        public override void Update(CommonAttackComponent self)
        {
            self.Update();
        }
    }

    public static class CommonAttackComponentSystem
    {
        public static void SetAttackTarget(this CommonAttackComponent self, Unit targetUnit)
        {
            if (targetUnit == null)
            {
                Log.Error("普攻组件接收到的targetUnit为null");
                return;
            }

            if (targetUnit.GetComponent<B2S_RoleCastComponent>()?.RoleCast == RoleCast.Adverse)
            {
                if (self.CachedUnitForAttack != targetUnit)
                {
                    self.CancelCommonAttack();
                }

                self.CachedUnitForAttack = targetUnit;

                self.m_StackFsmComponent.ChangeState<CommonAttackState>(StateTypes.CommonAttack, "CommonAttack", 1);
            }
        }

        private static async ETVoid StartCommonAttack(this CommonAttackComponent self)
        {
            self.CancellationTokenSource?.Cancel();
            self.CancellationTokenSource = new CancellationTokenSource();

            MessageHelper.Broadcast(new M2C_CommonAttack()
            {
                AttackCasterId = self.Entity.Id, TargetUnitId = self.CachedUnitForAttack.Id, CanAttack = true
            });
            await self.CommonAttack_Internal();
            //此次攻击完成
            self.CancellationTokenSource.Dispose();
            self.CancellationTokenSource = null;
        }

        private static async ETTask CommonAttack_Internal(this CommonAttackComponent self)
        {
            HeroDataComponent heroDataComponent = self.Entity.GetComponent<HeroDataComponent>();
            float attackPre = heroDataComponent.NodeDataForHero.OriAttackPre / (1 + heroDataComponent.NodeDataForHero.ExtAttackSpeed);
            float attackPos = heroDataComponent.NodeDataForHero.OriAttackPos / (1 + heroDataComponent.NodeDataForHero.ExtAttackSpeed);
            float attackSpeed = heroDataComponent.NodeDataForHero.OriAttackSpeed + heroDataComponent.NodeDataForHero.ExtAttackSpeed;

            //播放动画，如果动画播放完成还不能进行下一次普攻，则播放空闲动画
            await TimerComponent.Instance.WaitAsync((long) (attackPre * 1000), self.CancellationTokenSource.Token);

            List<NP_RuntimeTree> targetSkillCanvas = self.Entity.GetComponent<SkillCanvasManagerComponent>().GetSkillCanvas(10001);
            foreach (var skillCanva in targetSkillCanvas)
            {
                skillCanva.GetBlackboard().Set("CastNormalAttack", true);
                skillCanva.GetBlackboard().Set("NormalAttackUnitIds", new List<long>() { self.CachedUnitForAttack.Id });
            }

            Game.EventSystem.Run(EventIdType.ChangeHP, self.CachedUnitForAttack.Id, -50.0f);

            CDComponent.Instance.TriggerCD(self.Entity.Id, "CommonAttack");
            CDInfo commonAttackCDInfo = CDComponent.Instance.GetCDData(self.Entity.Id, "CommonAttack");
            commonAttackCDInfo.Interval = (long) (1 / attackSpeed - attackPre) * 1000;

            await TimerComponent.Instance.WaitAsync(commonAttackCDInfo.Interval, self.CancellationTokenSource.Token);
        }

        public static void Update(this CommonAttackComponent self)
        {
            if (self.Entity.GetComponent<StackFsmComponent>().GetCurrentFsmState().StateTypes == StateTypes.CommonAttack)
            {
                if (self.CachedUnitForAttack != null && !self.CachedUnitForAttack.IsDisposed)
                {
                    Vector3 selfUnitPos = (self.Entity as Unit).Position;
                    double distance = Vector3.Distance(selfUnitPos, self.CachedUnitForAttack.Position);
                    //目标距离大于当前攻击距离会先进行寻路，这里的1.75为175码
                    if (distance >= 1.75f)
                    {
                        if (!CDComponent.Instance.GetCDResult(self.Entity.Id, "MoveToAttack")) return;

                        CDComponent.Instance.TriggerCD(self.Entity.Id, "MoveToAttack");
                        self.IsMoveToTarget = true;

                        //移动完进入攻击状态
                        CommonAttackState commonAttackState = ReferencePool.Acquire<CommonAttackState>();
                        commonAttackState.SetData(StateTypes.CommonAttack, "CommonAttack", 1);

                        self.Entity.GetComponent<UnitPathComponent>()
                                .NavigateTodoSomething(self.CachedUnitForAttack.Position, 1.75f, commonAttackState);
                    }
                    else
                    {
                        //目标不为空，且处于攻击状态，且上次攻击已完成或取消
                        if ((self.CancellationTokenSource == null || self.CancellationTokenSource.IsCancellationRequested))
                        {
                            if (distance <= 1.75 && CDComponent.Instance.GetCDResult(self.Entity.Id, "CommonAttack"))
                                self.StartCommonAttack().Coroutine();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 取消攻击但不重置攻击对象
        /// </summary>
        public static void CancelCommonAttackWithOutResetTarget(this CommonAttackComponent self)
        {
            self.CancellationTokenSource?.Cancel();
            self.CancellationTokenSource = null;
            //MessageHelper.Broadcast(new M2C_CancelAttack() { UnitId = self.Entity.Id });
        }

        /// <summary>
        /// 取消攻击并且重置攻击对象
        /// </summary>
        public static void CancelCommonAttackExtension(this CommonAttackComponent self)
        {
            self.CancelCommonAttackWithOutResetTarget();
            self.CachedUnitForAttack = null;
        }
    }
}