using ET;
using ProtoBuf;
using System.Collections.Generic;

namespace ET
{
    [global::ProtoBuf.ProtoContract()]
    public partial class TMsg
    {
        [global::ProtoBuf.ProtoMember(1, DataFormat = global::ProtoBuf.DataFormat.FixedSize, IsRequired = true)]
        public uint type { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public byte[] body
        {
            get { return __pbn__body; }
            set { __pbn__body = value; }
        }

        public bool ShouldSerializebody() => __pbn__body != null;
        public void Resetbody() => __pbn__body = null;
        private byte[] __pbn__body;

        [global::ProtoBuf.ProtoMember(3)]
        public int error_code
        {
            get { return __pbn__error_code.GetValueOrDefault(); }
            set { __pbn__error_code = value; }
        }

        public bool ShouldSerializeerror_code() => __pbn__error_code != null;
        public void Reseterror_code() => __pbn__error_code = null;
        private int? __pbn__error_code;

        [global::ProtoBuf.ProtoMember(4)]
        [global::System.ComponentModel.DefaultValue("")]
        public string error_string
        {
            get { return __pbn__error_string ?? ""; }
            set { __pbn__error_string = value; }
        }

        public bool ShouldSerializeerror_string() => __pbn__error_string != null;
        public void Reseterror_string() => __pbn__error_string = null;
        private string __pbn__error_string;
    }

    [global::ProtoBuf.ProtoContract()]
    [Message(OuterOpcode_Realm.register_user_c2s)]
    public partial class register_user_c2s:Object,IMessage
    {
        [global::ProtoBuf.ProtoMember(1, IsRequired = true)]
        public int device_type { get; set; }

        [global::ProtoBuf.ProtoMember(2, IsRequired = true)]
        public string device_model { get; set; }

        [global::ProtoBuf.ProtoMember(3, IsRequired = true)]
        public string device_product_id { get; set; }

        [global::ProtoBuf.ProtoMember(8)]
        [global::System.ComponentModel.DefaultValue("")]
        public string desc
        {
            get { return __pbn__desc ?? ""; }
            set { __pbn__desc = value; }
        }

        public bool ShouldSerializedesc() => __pbn__desc != null;
        public void Resetdesc() => __pbn__desc = null;
        private string __pbn__desc;
    }

    [global::ProtoBuf.ProtoContract()]
    [Message(OuterOpcode_Realm.register_user_s2c)]
    public partial class register_user_s2c:Object,IMessage
    {
        [global::ProtoBuf.ProtoMember(1, IsRequired = true)]
        public int user_id { get; set; }
    }

    [global::ProtoBuf.ProtoContract()]
    [Message(OuterOpcode_Realm.get_transfer_endpoint_c2s)]
    public partial class get_transfer_endpoint_c2s:Object,IMessage
    {
        [global::ProtoBuf.ProtoMember(1, IsRequired = true)]
        public int user_id { get; set; }

        [global::ProtoBuf.ProtoMember(2, IsRequired = true)]
        public int endpoint_id { get; set; }
    }

    [global::ProtoBuf.ProtoContract()]
    [Message(OuterOpcode_Realm.get_transfer_endpoint_s2c)]
    public partial class get_transfer_endpoint_s2c:Object,IMessage
    {
        [global::ProtoBuf.ProtoMember(1, IsRequired = true)]
        public string ip { get; set; }

        [global::ProtoBuf.ProtoMember(2, IsRequired = true)]
        public int port { get; set; }

        [global::ProtoBuf.ProtoMember(3, IsRequired = true)]
        public int endpoint_id { get; set; }
    }
    
        [global::ProtoBuf.ProtoContract()]
        [Message(OuterOpcode_Realm.authenticate_c2s)]
    public partial class authenticate_c2s:Object,IMessage
    {
        [global::ProtoBuf.ProtoMember(1, IsRequired = true)]
        public int user_id { get; set; }

        [global::ProtoBuf.ProtoMember(2, IsRequired = true)]
        public int device_type { get; set; }

        [global::ProtoBuf.ProtoMember(3, IsRequired = true)]
        public string device_product_id { get; set; }

    }

    [global::ProtoBuf.ProtoContract()]
    [Message(OuterOpcode_Realm.authenticate_s2c)]
    public partial class authenticate_s2c:Object,IMessage
    {
    }

    [global::ProtoBuf.ProtoContract()]
    [Message(OuterOpcode_Realm.heartbeat_c2s)]
    public partial class heartbeat_c2s:Object,IMessage
    {
    }

    [global::ProtoBuf.ProtoContract()]
    [Message(OuterOpcode_Realm.all_sync_s2c)]
    public partial class all_sync_s2c:Object,IMessage
    {
        [global::ProtoBuf.ProtoMember(1, IsRequired = true)]
        public int house_type { get; set; }

        [global::ProtoBuf.ProtoMember(2, IsRequired = true)]
        public int music_id { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        public int[] on_lighting_ids { get; set; }

        [global::ProtoBuf.ProtoMember(4)]
        public int[] on_dj_ids { get; set; }

        [global::ProtoBuf.ProtoMember(5)]
        public int[] dj_playerids { get; set; }

        [global::ProtoBuf.ProtoMember(6, TypeName = "metalife.disco.transfer.player")]
        public global::System.Collections.Generic.List<player> players { get; } = new global::System.Collections.Generic.List<player>();

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class player:Object,IMessage
    {
        [global::ProtoBuf.ProtoMember(1, IsRequired = true)]
        public int player_id { get; set; }

        [global::ProtoBuf.ProtoMember(2, IsRequired = true)]
        public float x { get; set; }

        [global::ProtoBuf.ProtoMember(3, IsRequired = true)]
        public float y { get; set; }

        [global::ProtoBuf.ProtoMember(4, IsRequired = true)]
        public int is_dj { get; set; }

        [global::ProtoBuf.ProtoMember(5, IsRequired = true)]
        public float big_factor { get; set; }

        [global::ProtoBuf.ProtoMember(6, IsRequired = true)]
        public int figure_id { get; set; }

        [global::ProtoBuf.ProtoMember(7, IsRequired = true)]
        public string player_name { get; set; }

    }

    [global::ProtoBuf.ProtoContract()]
    [Message(OuterOpcode_Realm.player_enter_s2c)]
    public partial class player_enter_s2c:Object,IMessage
    {
        [global::ProtoBuf.ProtoMember(1, IsRequired = true)]
        public player one_player { get; set; }

    }

    [global::ProtoBuf.ProtoContract()]
    [Message(OuterOpcode_Realm.player_enter_s2c)]
    public partial class player_leave_s2c:Object,IMessage
    {
        [global::ProtoBuf.ProtoMember(1, IsRequired = true)]
        public int player_id { get; set; }

    }

    [global::ProtoBuf.ProtoContract()]
    [Message(OuterOpcode_Realm.action_req_c2s)]
    public partial class action_req_c2s:Object,IMessage
    {
        [global::ProtoBuf.ProtoMember(1, IsRequired = true)]
        public int action_id { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public int int1
        {
            get { return __pbn__int1.GetValueOrDefault(); }
            set { __pbn__int1 = value; }
        }
        public bool ShouldSerializeint1() => __pbn__int1 != null;
        public void Resetint1() => __pbn__int1 = null;
        private int? __pbn__int1;

        [global::ProtoBuf.ProtoMember(3)]
        public int int2
        {
            get { return __pbn__int2.GetValueOrDefault(); }
            set { __pbn__int2 = value; }
        }
        public bool ShouldSerializeint2() => __pbn__int2 != null;
        public void Resetint2() => __pbn__int2 = null;
        private int? __pbn__int2;

        [global::ProtoBuf.ProtoMember(4)]
        public float float1
        {
            get { return __pbn__float1.GetValueOrDefault(); }
            set { __pbn__float1 = value; }
        }
        public bool ShouldSerializefloat1() => __pbn__float1 != null;
        public void Resetfloat1() => __pbn__float1 = null;
        private float? __pbn__float1;

        [global::ProtoBuf.ProtoMember(5)]
        public float float2
        {
            get { return __pbn__float2.GetValueOrDefault(); }
            set { __pbn__float2 = value; }
        }
        public bool ShouldSerializefloat2() => __pbn__float2 != null;
        public void Resetfloat2() => __pbn__float2 = null;
        private float? __pbn__float2;

    }

    [global::ProtoBuf.ProtoContract()]
    [Message(OuterOpcode_Realm.action_req_s2c)]
    public partial class action_req_s2c:Object,IMessage
    {
        [global::ProtoBuf.ProtoMember(1, IsRequired = true)]
        public int action_id { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public int int1
        {
            get { return __pbn__int1.GetValueOrDefault(); }
            set { __pbn__int1 = value; }
        }
        public bool ShouldSerializeint1() => __pbn__int1 != null;
        public void Resetint1() => __pbn__int1 = null;
        private int? __pbn__int1;

        [global::ProtoBuf.ProtoMember(3)]
        public int int2
        {
            get { return __pbn__int2.GetValueOrDefault(); }
            set { __pbn__int2 = value; }
        }
        public bool ShouldSerializeint2() => __pbn__int2 != null;
        public void Resetint2() => __pbn__int2 = null;
        private int? __pbn__int2;

        [global::ProtoBuf.ProtoMember(4)]
        public float float1
        {
            get { return __pbn__float1.GetValueOrDefault(); }
            set { __pbn__float1 = value; }
        }
        public bool ShouldSerializefloat1() => __pbn__float1 != null;
        public void Resetfloat1() => __pbn__float1 = null;
        private float? __pbn__float1;

        [global::ProtoBuf.ProtoMember(5)]
        public float float2
        {
            get { return __pbn__float2.GetValueOrDefault(); }
            set { __pbn__float2 = value; }
        }
        public bool ShouldSerializefloat2() => __pbn__float2 != null;
        public void Resetfloat2() => __pbn__float2 = null;
        private float? __pbn__float2;

    }

    [global::ProtoBuf.ProtoContract()]
    [Message(OuterOpcode_Realm.action_syn_s2c)]
    public partial class action_syn_s2c:Object,IMessage
    {
        [global::ProtoBuf.ProtoMember(1, IsRequired = true)]
        public int player_id { get; set; }

        [global::ProtoBuf.ProtoMember(2, IsRequired = true)]
        public int action_id { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        public int int1
        {
            get { return __pbn__int1.GetValueOrDefault(); }
            set { __pbn__int1 = value; }
        }
        public bool ShouldSerializeint1() => __pbn__int1 != null;
        public void Resetint1() => __pbn__int1 = null;
        private int? __pbn__int1;

        [global::ProtoBuf.ProtoMember(4)]
        public int int2
        {
            get { return __pbn__int2.GetValueOrDefault(); }
            set { __pbn__int2 = value; }
        }
        public bool ShouldSerializeint2() => __pbn__int2 != null;
        public void Resetint2() => __pbn__int2 = null;
        private int? __pbn__int2;

        [global::ProtoBuf.ProtoMember(5)]
        public float float1
        {
            get { return __pbn__float1.GetValueOrDefault(); }
            set { __pbn__float1 = value; }
        }
        public bool ShouldSerializefloat1() => __pbn__float1 != null;
        public void Resetfloat1() => __pbn__float1 = null;
        private float? __pbn__float1;

        [global::ProtoBuf.ProtoMember(6)]
        public float float2
        {
            get { return __pbn__float2.GetValueOrDefault(); }
            set { __pbn__float2 = value; }
        }
        public bool ShouldSerializefloat2() => __pbn__float2 != null;
        public void Resetfloat2() => __pbn__float2 = null;
        private float? __pbn__float2;

    }
    
}

