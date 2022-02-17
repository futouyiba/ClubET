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

    public interface IClubHandler
    {
        void Handle(Session session, int errorCode, object innerMessage);
        Type GetMessageType();

        Type GetResponseType();
    }
}