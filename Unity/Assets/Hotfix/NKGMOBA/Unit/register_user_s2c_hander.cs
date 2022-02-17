using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace ET
{
    [MessageHandler]
    public class register_user_s2c_handler : AMHandler<register_user_s2c>
    {
        protected override async ETVoid Run(Session session, register_user_s2c message)
        {
            // Debug.Log($"register s2c message:{JsonHelper.ToJson(message)}");
            Debug.Log($"register s2c message:{message}");
            PlayerPrefs.SetInt(RegisterHelper.USER_ID, message.user_id);
            Debug.Log("preparing to get endpoint...");
            
            await ETTask.CompletedTask;
        }
    }
}