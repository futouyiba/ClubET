﻿using System.Collections.Generic;

namespace ET
{
    public static class NumericComponentSystem
    {
        /// <summary>
        /// 初始化初始值字典，用于值回退，比如一个buff加50ad，buff移除后要减去这个50ad，就需要用到OriNumericDic里的值
        /// </summary>
        public static void InitOriNumerDic(this NumericComponent self)
        {
            self.OriNumericDic = new Dictionary<int, float>(self.NumericDic);
        }

        public static float GetOriData(this NumericComponent self, NumericType numericType)
        {
            return self.OriNumericDic[(int) numericType];
        }

#if SERVER
        /// <summary>
        /// 适配变化
        /// </summary>
        public static void ApplyChange(this NumericComponent self, NumericType numericType, float changedValue)
        {
            Unit unit = self.GetParent<Unit>();
            MessageHelper.BroadcastToRoom(unit.BelongToRoom,
                new M2C_ChangeUnitAttribute()
                    {UnitId = unit.Id, NumericType = (int) numericType, ChangeValue = changedValue});
            self[numericType] += changedValue;
        }
#endif

        public static void SetValueWithoutBroadCast(this NumericComponent self, NumericType numericType, float value)
        {
            self.NumericDic[(int) numericType] = value;
        }

        public static Dictionary<int, float> GetOriNum(this NumericComponent self)
        {
            return self.OriNumericDic;
        }
    }
}