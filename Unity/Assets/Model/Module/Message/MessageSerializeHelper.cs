using System;
using System.IO;
using MongoDB.Bson.IO;

namespace ET
{
    public static class MessageSerializeHelper
    {
        public const ushort PbMaxOpcode = 55000;
        
        public const ushort JsonMinOpcode = 61000;
        
        public static object DeserializeFrom(ushort opcode, Type type, MemoryStream memoryStream)
        {
            if (opcode < PbMaxOpcode)
            {
                return ProtobufHelper.FromStream(type, memoryStream);
            }
            
            if (opcode >= JsonMinOpcode)
            {
                return JsonHelper.FromJson(type, memoryStream.GetBuffer().ToStr((int)memoryStream.Position, (int)(memoryStream.Length - memoryStream.Position)));
            }
#if SERVER
            return MongoHelper.FromStream(type, memoryStream);
#else
            throw new Exception($"client no message: {opcode}");
#endif
        }

        public static void SerializeTo(ushort opcode, object obj, MemoryStream memoryStream, int RpcId=-1)
        {
            if (opcode < PbMaxOpcode)
            {
                var bodyBytes = ProtobufHelper.ToBytes(obj);
                var tMsg = new TMsg()
                {
                    body = bodyBytes,
                    type = opcode,
                    rpc_id = RpcId,
                };
                
                ProtobufHelper.ToStream(tMsg, memoryStream);
                return;
            }

            if (opcode >= JsonMinOpcode)
            {
                string s = JsonHelper.ToJson(obj);
                byte[] bytes = s.ToUtf8();
                memoryStream.Write(bytes, 0, bytes.Length);
                return;
            }
#if SERVER
            MongoHelper.ToStream(obj, memoryStream);
#else
            throw new Exception($"client no message: {opcode}");
#endif
        }

        public static MemoryStream GetStream(int count = 0)
        {
            MemoryStream stream;
            if (count > 0)
            {
                stream = new MemoryStream(count);
            }
            else
            {
                stream = new MemoryStream();
            }

            return stream;
        }
        
        public static (ushort, MemoryStream) MessageToStream(object message,int rpcId=-1, int count = 0)
        {
            // MemoryStream stream = GetStream(Packet.OpcodeLength + count);
            MemoryStream stream = GetStream(count);

            ushort opcode = OpcodeTypeComponent.Instance.GetOpcode(message.GetType());
            
            // stream.Seek(Packet.OpcodeLength, SeekOrigin.Begin);
            // stream.SetLength(Packet.OpcodeLength);
            
            // stream.GetBuffer().WriteTo(0, opcode);
            
            MessageSerializeHelper.SerializeTo(opcode, message, stream, rpcId);
            
            stream.Seek(0, SeekOrigin.Begin);
            return (opcode, stream);
        }
        
        public static (ushort, MemoryStream) MessageToStream(long actorId, object message, int count = 0)
        {
            int actorSize = sizeof (long);
            MemoryStream stream = GetStream(actorSize + Packet.OpcodeLength + count);

            ushort opcode = OpcodeTypeComponent.Instance.GetOpcode(message.GetType());
            
            stream.Seek(actorSize + Packet.OpcodeLength, SeekOrigin.Begin);
            stream.SetLength(actorSize + Packet.OpcodeLength);

            // 写入actorId
            stream.GetBuffer().WriteTo(0, actorId);
            stream.GetBuffer().WriteTo(actorSize, opcode);
            
            MessageSerializeHelper.SerializeTo(opcode, message, stream);
            
            stream.Seek(0, SeekOrigin.Begin);
            return (opcode, stream);
        }
    }
}