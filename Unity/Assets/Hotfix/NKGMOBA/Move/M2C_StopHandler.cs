﻿using UnityEngine;

namespace ET
{
	[MessageHandler]
	public class M2C_StopHandler : AMHandler<M2C_Stop>
	{
		protected override async ETVoid Run(Session session, M2C_Stop message)
		{
			Unit unit = session.DomainScene().GetComponent<RoomManagerComponent>().GetOrCreateBattleRoom().GetComponent<UnitComponent>().Get(message.Id);
			
			if (unit == null)
			{
				return;
			}

            Vector3 pos = new Vector3(message.X, message.Y, message.Z);
            Quaternion rotation = new Quaternion(message.A, message.B, message.C, message.W);

            MoveComponent moveComponent = unit.GetComponent<MoveComponent>();
            moveComponent.Stop();
            Game.EventSystem.Publish(new EventType.MoveStop() { Unit = unit }).Coroutine();
            unit.Position = pos;
            unit.Rotation = rotation;
            unit.GetComponent<ObjectWait>()?.Notify(new WaitType.Wait_UnitStop() { Error = message.Error });
            await ETTask.CompletedTask;
        }
	}
}
