﻿namespace ET
{
	public class PlayerComponent: Entity
	{
		public Session GateSession;
		public Session LobbySession;
		public Session RealmSession;
		public Room BelongToRoom;

		/// <summary>
		/// 已经完成的加载次数
		/// </summary>
		public int HasCompletedLoadCount;
		
		public long PlayerId;
		public string PlayerAccount;
	}
}
