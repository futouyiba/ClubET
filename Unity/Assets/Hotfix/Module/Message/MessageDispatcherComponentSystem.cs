﻿using System;
using System.Collections.Generic;

namespace ET
{
    [ObjectSystem]
    public class MessageDispatcherComponentAwakeSystem: AwakeSystem<MessageDispatcherComponent>
    {
        public override void Awake(MessageDispatcherComponent self)
        {
            MessageDispatcherComponent.Instance = self;
            self.Load();
        }
    }

    [ObjectSystem]
    public class MessageDispatcherComponentLoadSystem: LoadSystem<MessageDispatcherComponent>
    {
        public override void Load(MessageDispatcherComponent self)
        {
            self.Load();
        }
    }

    [ObjectSystem]
    public class MessageDispatcherComponentDestroySystem: DestroySystem<MessageDispatcherComponent>
    {
        public override void Destroy(MessageDispatcherComponent self)
        {
            MessageDispatcherComponent.Instance = null;
            self.Handlers.Clear();
        }
    }

    /// <summary>
    /// 消息分发组件
    /// </summary>
    public static class MessageDispatcherComponentHelper
    {
        public static void Load(this MessageDispatcherComponent self)
        {
            self.Handlers.Clear();

            HashSet<Type> types = Game.EventSystem.GetTypes(typeof (MessageHandlerAttribute));

            foreach (Type type in types)
            {
                IClubHandler iMHandler = Activator.CreateInstance(type) as IClubHandler;
                if (iMHandler == null)
                {
                    Log.Error($"message handle {type.Name} 需要继承 IClubHandler");
                    continue;
                }

                Type messageType = iMHandler.GetMessageType();
                ushort opcode = OpcodeTypeComponent.Instance.GetOpcode(messageType);
                if (opcode == 0)
                {
                    Log.Error($"消息opcode为0: {messageType.Name}");
                    continue;
                }

                self.RegisterHandler(opcode, iMHandler);
            }
        }

        public static void RegisterHandler(this MessageDispatcherComponent self, ushort opcode, IClubHandler handler)
        {
            if (!self.Handlers.ContainsKey(opcode))
            {
                self.Handlers.Add(opcode, new List<IClubHandler>());
            }

            self.Handlers[opcode].Add(handler);
        }

        public static void Handle(this MessageDispatcherComponent self, Session session, ushort opcode, object message)
        {
            if (!self.Handlers.TryGetValue(opcode, out var actions))
            {
                Log.Error($"消息没有处理: {opcode} {message.GetType()} {message}");
                return;
            }

            foreach (IClubHandler ev in actions)
            {
                try
                {
                    ev.Handle(session, message);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }
        
        public static void HandleError(this MessageDispatcherComponent self, Session session, ushort opcode, int errorCode, object message)
        {
            if (!self.Handlers.TryGetValue(opcode, out var actions))
            {
                Log.Error($"消息没有处理: {opcode} {message.GetType()} {message}");
                return;
            }

            foreach (IClubHandler ev in actions)
            {
                try
                {
                    ev.HandleError(session, errorCode, message);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }
    }
}