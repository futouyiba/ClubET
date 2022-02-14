using System;
using System.IO;
using UnityEngine;

namespace ET
{
    public class OuterMessageDispatcher: IMessageDispatcher
    {
        // 查找卡死问题临时处理
        public long lastMessageTime = long.MaxValue;
        public object LastMessage;
        
        public void Dispatch(Session session, MemoryStream memoryStream)
        {
            var tMsg = ProtobufHelper.FromStream(typeof(TMsg), memoryStream) as TMsg;
            if (tMsg == null)
            {
                Debug.LogError("收到一条不是我们定义tmsg的消息，跳过！");
                return;
            }
            var opcode = (ushort)tMsg.type;
            
            // ushort opcode = BitConverter.ToUInt16(memoryStream.GetBuffer(), Packet.KcpOpcodeIndex);
            Type type = OpcodeTypeComponent.Instance.GetType(opcode);
            object message = null;
            if (tMsg.body!=null)
            {
                message = ProtobufHelper.FromBytes(type, tMsg.body, 0, tMsg.body.Length);
                OpcodeHelper.LogMsg(session.DomainZone(), opcode, message);

            }

            if (tMsg.error_code !=0)
            {
                message = null;
            }
            // object message = MessageSerializeHelper.DeserializeFrom(opcode, type, memoryStream);

            if (TimeHelper.ClientFrameTime() - this.lastMessageTime > 3000)
            {
                Log.Info($"可能导致卡死的消息: {this.LastMessage}");
            }

            this.lastMessageTime = TimeHelper.ClientFrameTime();
            this.LastMessage = message;
            
            if (message is IResponse response)
            {
                session.OnRead((ushort)opcode, response);
                return;
            }

            // 普通消息或者是Rpc请求消息
            MessageDispatcherComponent.Instance.Handle(session, opcode, message);
        }
    }
}