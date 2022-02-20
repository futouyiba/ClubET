using System;
using UnityEngine;

namespace ET
{
    [MessageHandler]
    public class authenticate_s2c_handler : AClubHandler<authenticate_s2c>
    {
        // protected override async ETVoid Run(Session session, get_transfer_endpoint_s2c message)
        // {
        //     Debug.Log(message);
        //     await ETTask.CompletedTask;
        // }

        protected override async ETVoid Run(Session session, authenticate_s2c message)
        {
            Debug.Log(message);
            await ETTask.CompletedTask;
        }

        protected override async ETVoid RunError(Session session, int errorCode, object innerMessage)
        {
            Debug.Log($"authenticate error, error code is:{errorCode},message is {innerMessage}");
            await ETTask.CompletedTask;
        }
    }
}