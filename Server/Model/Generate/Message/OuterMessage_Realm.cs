using ET;
using ProtoBuf;
using System.Collections.Generic;
namespace ET
{
	[ResponseType(typeof(R2C_Login))]
	[Message(OuterOpcode_Realm.C2R_Login)]
	[ProtoContract]
	public partial class C2R_Login: Object, IRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public string Account { get; set; }

		[ProtoMember(2)]
		public string Password { get; set; }

	}

	[Message(OuterOpcode_Realm.R2C_Login)]
	[ProtoContract]
	public partial class R2C_Login: Object, IResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public string GateAddress { get; set; }

		[ProtoMember(2)]
		public long Key { get; set; }

		[ProtoMember(3)]
		public long GateId { get; set; }

	}

	[ResponseType(typeof(R2C_Registe))]
	[Message(OuterOpcode_Realm.C2R_Registe)]
	[ProtoContract]
	public partial class C2R_Registe: Object, IRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public string Account { get; set; }

		[ProtoMember(2)]
		public string Password { get; set; }

	}

	[Message(OuterOpcode_Realm.R2C_Registe)]
	[ProtoContract]
	public partial class R2C_Registe: Object, IResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

	}

	[ResponseType(typeof(TMsg))]
	[Message(OuterOpcode_Realm.TMsg)]
	[ProtoContract]
	public partial class TMsg: Object, IMessage
	{
/// 消息类型
/// 消息类型
		[ProtoMember(1)]
		public int type { get; set; }

/// 消息体
/// 消息体
		[ProtoMember(2)]
		public byte[] body { get; set; }

		[ProtoMember(3)]
		public int rpc_id { get; set; }

///
///
/// 错误信息：如果消息执行错误，则回复错误编码和描述
/// 错误信息：如果消息执行错误，则回复错误编码和描述
/// error_code = 0 表示成功，否则失败
/// error_code = 0 表示成功，否则失败
///
///
		[ProtoMember(4)]
		public int error_code { get; set; }

		[ProtoMember(5)]
		public string error_string { get; set; }

	}

	[ResponseType(typeof(register_user_s2c))]
	[Message(OuterOpcode_Realm.register_user_c2s)]
	[ProtoContract]
	public partial class register_user_c2s: Object, IMessage
// IRequest
// IRequest
	{
// int32 RpcId = 90;
// int32 RpcId = 90;
		[ProtoMember(1)]
		public int device_type { get; set; }

		[ProtoMember(2)]
		public string device_model { get; set; }

		[ProtoMember(3)]
		public string device_product_id { get; set; }

		[ProtoMember(8)]
		public string desc { get; set; }

	}

/// 返回
	[Message(OuterOpcode_Realm.register_user_s2c)]
	[ProtoContract]
	public partial class register_user_s2c: Object, IMessage
// IResponse
// IResponse
	{
// int32 RpcId = 90;
// int32 RpcId = 90;
		[ProtoMember(1)]
		public int user_id { get; set; }

	}

///
/// 获取数据中转服务endpoint
///
	[ResponseType(typeof(get_transfer_endpoint_s2c))]
	[Message(OuterOpcode_Realm.get_transfer_endpoint_c2s)]
	[ProtoContract]
	public partial class get_transfer_endpoint_c2s: Object, IMessage
// IRequest
// IRequest
	{
// int32 RpcId = 90;
// int32 RpcId = 90;
		[ProtoMember(1)]
		public int user_id { get; set; }

		[ProtoMember(2)]
		public int endpoint_id { get; set; }

	}

/// 返回
	[Message(OuterOpcode_Realm.get_transfer_endpoint_s2c)]
	[ProtoContract]
	public partial class get_transfer_endpoint_s2c: Object, IMessage
// IResponse
// IResponse
	{
// int32 RpcId = 90;
// int32 RpcId = 90;
		[ProtoMember(1)]
		public string ip { get; set; }

		[ProtoMember(2)]
		public int port { get; set; }

		[ProtoMember(3)]
		public int endpoint_id { get; set; }

	}

	[Message(OuterOpcode_Realm.authenticate_c2s)]
	[ProtoContract]
	public partial class authenticate_c2s: Object, IMessage
	{
		[ProtoMember(1)]
		public int user_id { get; set; }

		[ProtoMember(2)]
		public int device_type { get; set; }

		[ProtoMember(3)]
		public string device_product_id { get; set; }

	}

	[Message(OuterOpcode_Realm.authenticate_s2c)]
	[ProtoContract]
	public partial class authenticate_s2c: Object, IMessage
	{
	}

///
/// 心跳消息   定期30秒发一次。若30秒内没发
///
	[Message(OuterOpcode_Realm.heartbeat_c2s)]
	[ProtoContract]
	public partial class heartbeat_c2s: Object, IMessage
	{
	}

///
/// 全状态同步消息
///
	[Message(OuterOpcode_Realm.all_sync_s2c)]
	[ProtoContract]
	public partial class all_sync_s2c: Object, IMessage
	{
		[ProtoMember(1)]
		public int house_type { get; set; }

		[ProtoMember(2)]
		public int music_id { get; set; }

		[ProtoMember(3)]
		public List<int> on_lighting_ids = new List<int>();

		[ProtoMember(4)]
		public List<int> on_dj_ids = new List<int>();

		[ProtoMember(5)]
		public List<int> dj_playerids = new List<int>();

		[ProtoMember(6)]
		public List<player> players = new List<player>();

	}

	[Message(OuterOpcode_Realm.player)]
	[ProtoContract]
	public partial class player: Object
	{
		[ProtoMember(1)]
		public int player_id { get; set; }

		[ProtoMember(2)]
		public float x { get; set; }

		[ProtoMember(3)]
		public float y { get; set; }

		[ProtoMember(4)]
		public int is_dj { get; set; }

		[ProtoMember(5)]
		public float big_factor { get; set; }

		[ProtoMember(6)]
		public int figure_id { get; set; }

		[ProtoMember(7)]
		public string player_name { get; set; }

	}

///
/// 玩家进入消息
///
	[Message(OuterOpcode_Realm.player_enter_s2c)]
	[ProtoContract]
	public partial class player_enter_s2c: Object, IMessage
	{
		[ProtoMember(1)]
		public player one_player { get; set; }

	}

///
/// 玩家离开消息
///
	[Message(OuterOpcode_Realm.player_leave_s2c)]
	[ProtoContract]
	public partial class player_leave_s2c: Object, IMessage
	{
		[ProtoMember(1)]
		public int player_id { get; set; }

	}

///
/// 动作请求消息
///
	[Message(OuterOpcode_Realm.action_req_c2s)]
	[ProtoContract]
	public partial class action_req_c2s: Object, IMessage
	{
		[ProtoMember(1)]
		public int action_id { get; set; }

		[ProtoMember(2)]
		public int int1 { get; set; }

		[ProtoMember(3)]
		public int int2 { get; set; }

		[ProtoMember(4)]
		public float float1 { get; set; }

		[ProtoMember(5)]
		public float float2 { get; set; }

	}

	[Message(OuterOpcode_Realm.action_req_s2c)]
	[ProtoContract]
	public partial class action_req_s2c: Object, IMessage
	{
// 这个不是广播的。
// 这个不是广播的。
		[ProtoMember(1)]
		public int action_id { get; set; }

		[ProtoMember(2)]
		public int int1 { get; set; }

		[ProtoMember(3)]
		public int int2 { get; set; }

		[ProtoMember(4)]
		public float float1 { get; set; }

		[ProtoMember(5)]
		public float float2 { get; set; }

	}

///
/// 动作同步消息
///
	[Message(OuterOpcode_Realm.action_syn_s2c)]
	[ProtoContract]
	public partial class action_syn_s2c: Object, IMessage
	{
		[ProtoMember(1)]
		public int player_id { get; set; }

		[ProtoMember(2)]
		public int action_id { get; set; }

		[ProtoMember(3)]
		public int int1 { get; set; }

		[ProtoMember(4)]
		public int int2 { get; set; }

		[ProtoMember(5)]
		public float float1 { get; set; }

		[ProtoMember(6)]
		public float float2 { get; set; }

	}

}
