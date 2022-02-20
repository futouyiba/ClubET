using System;

namespace ET
{
    [MessageHandler]
    public abstract class AMHandler<Message>: IMHandler where Message : class
    {
        protected abstract ETVoid Run(Session session, Message message);

        public void Handle(Session session, object msg)
        {
            Message message = msg as Message;
            // if (message == null)
            // {
            //     Log.Error($"消息类型转换错误: {msg.GetType().Name} to {typeof (Message).Name}");
            //     return;
            // }

            if (session.IsDisposed)
            {
                Log.Error($"session disconnect {msg}");
                return;
            }

            this.Run(session, message).Coroutine();
        }

        public Type GetMessageType()
        {
            return typeof (Message);
        }

        public Type GetResponseType()
        {
            return null;
        }
    }
    
    [MessageHandler]
    public abstract class AClubHandler<Message>: IClubHandler where Message : class
    {
        protected abstract ETVoid Run(Session session, Message message);

        public void Handle(Session session, object msg)
        {
            Message message = msg as Message;
            if (message == null)
            {
                Log.Error($"消息类型转换错误: {msg.GetType().Name} to {typeof (Message).Name}");
                return;
            }

            if (session.IsDisposed)
            {
                Log.Error($"session disconnect {msg}");
                return;
            }

            this.Run(session, message).Coroutine();
        }

        protected abstract ETVoid RunError(Session session,int errorCode, object innerMessage);

        public void HandleError(Session session, int errorCode, object innerMessage)
        {
            if (session.IsDisposed)
            {
                Log.Error($"session disconnect, handle error code: {errorCode}");
                return;
            }

            this.RunError(session,errorCode, innerMessage).Coroutine();
        }

        public Type GetMessageType()
        {
            return typeof (Message);
        }

        public Type GetResponseType()
        {
            return null;
        }
    }
}