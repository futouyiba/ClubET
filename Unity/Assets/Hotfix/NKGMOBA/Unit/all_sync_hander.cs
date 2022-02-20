using System;
using UnityEngine;

namespace ET
{
    [MessageHandler]
    public class all_sync_s2c_handler : AClubHandler<all_sync_s2c>
    {
        protected override async ETVoid Run(Session session, all_sync_s2c message)
        {
            // Debug.Log($"register s2c message:{JsonHelper.ToJson(message)}");
            Debug.Log($"all sync message:{message}");

            await ETTask.CompletedTask;
        }

        protected override async ETVoid RunError(Session session, int errorCode, object innerMessage)
        {
            Debug.Log($"all sync error, error code is:{errorCode}");
            await ETTask.CompletedTask;
        }
    }
}