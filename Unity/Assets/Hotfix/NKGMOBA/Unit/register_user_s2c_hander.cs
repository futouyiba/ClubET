using System;
using UnityEngine;

namespace ET
{
    [MessageHandler]
    public class register_user_s2c_handler : AClubHandler<register_user_s2c>
    {
        protected override async ETVoid Run(Session session, register_user_s2c message)
        {
            // Debug.Log($"register s2c message:{JsonHelper.ToJson(message)}");
            Debug.Log($"register s2c message:{message}");
            PlayerPrefs.SetInt(RegisterHelper.USER_ID, message.user_id);
            Debug.Log("preparing to get endpoint...");
            Game.Scene.GetComponent<PlayerComponent>().RealmSession.Send(new get_transfer_endpoint_c2s()
            {
                endpoint_id = 1,
                user_id = PlayerPrefs.GetInt(RegisterHelper.USER_ID),
            });
            
            await ETTask.CompletedTask;
        }

        protected override async ETVoid RunError(Session session, int errorCode, object innerMessage)
        {
            Debug.Log($"register error, error code is:{errorCode}");
            await RegisterHelper.Register(null, String.Empty, String.Empty, String.Empty);
        }
    }
}