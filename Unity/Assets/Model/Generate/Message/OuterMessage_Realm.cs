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
// int32 RpcId = 90;
// int32 RpcId = 90;
		[ProtoMember(1,DataFormat = ProtoBuf.DataFormat.FixedSize)]
		// [ProtoMember(1)]
		public uint type { get; set; }

/// 消息体
/// 消息体
		[ProtoMember(2)]
		public byte[] body { get; set; }

///
///
/// 错误信息：如果消息执行错误，则回复错误编码和描述
/// 错误信息：如果消息执行错误，则回复错误编码和描述
/// error_code = 0 表示成功，否则失败
/// error_code = 0 表示成功，否则失败
///
///
		[ProtoMember(3)]
		public int error_code { get; set; }

		[ProtoMember(4)]
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
		[ProtoMember(90)]
		public int RpcId { get; set; }

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
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public string ip { get; set; }

		[ProtoMember(2)]
		public int port { get; set; }

		[ProtoMember(3)]
		public int endpoint_id { get; set; }

	}

}
