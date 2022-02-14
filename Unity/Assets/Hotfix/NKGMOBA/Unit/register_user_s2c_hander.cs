using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace ET
{
    [MessageHandler]
    public class register_user_s2c_handler : AMHandler<register_user_s2c>
    {
        protected override async ETVoid Run(Session session, register_user_s2c message)
        {
            Debug.Log($"register s2c message:{JsonHelper.ToJson(message)}");

            await ETTask.CompletedTask;
        }
    }
}