using System;
using UnityEngine;

namespace ET
{
    [MessageHandler]
    public class get_transfer_endpoint_s2c_handler : AClubHandler<get_transfer_endpoint_s2c>
    {
        protected override async ETVoid Run(Session session, get_transfer_endpoint_s2c message)
        {
            await RegisterHelper.ConnectTransfer(message.ip, message.port);
            await ETTask.CompletedTask;
        }

        protected override async ETVoid RunError(Session session, int errorCode, object innerMessage)
        {
            Debug.Log($"register error, error code is:{errorCode}");
            await RegisterHelper.Register(null, String.Empty, String.Empty, String.Empty);
        }
    }
}