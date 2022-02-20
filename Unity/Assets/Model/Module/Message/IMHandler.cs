using System;

namespace ET
{
    public interface IMHandler
    {
        void Handle(Session session, object message);

        // void HandleError(Session session, object message);
        
        Type GetMessageType();

        Type GetResponseType();
    }

    public interface IClubHandler:IMHandler
    {
        void HandleError(Session session, int errorCode, object innerMessage);
    }
}