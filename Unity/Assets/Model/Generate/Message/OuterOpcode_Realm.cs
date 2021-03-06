namespace ET
{
	public static partial class OuterOpcode_Realm
	{
		public const ushort C2R_Login = 30001;
		public const ushort R2C_Login = 30002;
		public const ushort C2R_Registe = 30003;
		public const ushort R2C_Registe = 30004;
		public const ushort TMsg = 30005;
		public const ushort register_user_c2s = 10000;
		public const ushort register_user_s2c = 10001;
		public const ushort get_transfer_endpoint_c2s = 10002;
		public const ushort get_transfer_endpoint_s2c = 10003;
		public const ushort authenticate_c2s = 20000;
		public const ushort authenticate_s2c = 20001;
		public const ushort heartbeat_c2s = 20002;
		public const ushort all_sync_s2c = 20003;
		public const ushort player_enter_s2c = 20004;
		public const ushort player_leave_s2c = 20005;
		public const ushort action_req_c2s = 20006;
		public const ushort action_req_s2c = 20007;
		public const ushort action_syn_s2c = 20008;
	}
}
