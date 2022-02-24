using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;

namespace ET
{
    public class RegisterHelper
    { 
        public static string DEVICE_PRODUCT_ID = "DEVICE_PRODUCT_ID";
        public static string DEVICE_MODEL = "DEVICE_MODEL";
        public static string USER_ID = "USER_ID";
        public static int registerPostfix = 70;
        public static string CloudIp = "82.157.8.127";
        public static int port = 8800;
        public static Entity fuiComponent;
        public static async ETTask Register(Entity fuiComponent, string address, string account, string password)
        {
            try
            {
                if (fuiComponent!=null)
                {
                    RegisterHelper.fuiComponent = fuiComponent;
                }
                await RegisterHelper.Connect(fuiComponent, "");
                
                Scene zoneScene =RegisterHelper. fuiComponent.DomainScene();

                if (account != "")
                {
                    registerPostfix = int.Parse(account);
                }

                string postfixString = registerPostfix == 0 ? string.Empty : registerPostfix.ToString();

                // // if (account == "" || password == "")
                // if (account == "" )
                // {
                //     await Game.EventSystem.Publish(new EventType.LoginOrRegsiteFail()
                //         {ErrorMessage = "注册后缀不能为空", ZoneScene = zoneScene});
                //         // {ErrorMessage = "账号名或密码不能为空", ZoneScene = zoneScene});
                //     return;
                // }

                // R2C_Registe r2CRegiste;
                // FUI_LoadingComponent.ShowLoadingUI();
                PlayerPrefs.SetString(RegisterHelper.DEVICE_MODEL, DEVICE_MODEL + postfixString);
                PlayerPrefs.SetString(DEVICE_PRODUCT_ID, DEVICE_PRODUCT_ID+postfixString);
                var registerResp = (register_user_s2c)await Game.Scene.GetComponent<PlayerComponent>()  .RealmSession.Call(new register_user_c2s()
                {
                    device_type = 1,
                    device_model = DEVICE_MODEL + postfixString,
                    device_product_id = DEVICE_PRODUCT_ID + postfixString,
                });
                Debug.Log("rpc invoked! response:    "+registerResp);
                // var bytes = new byte[] {13,16,39,0,0,18,35,8,1,18,12,100,101,118,105,99,101,95,
                //     109,111,100,101,108,26,17,100,101,118,105,99,101,95,112,
                //     114,111,100,117,99,116,95,105,100};
                // var stream = new MemoryStream(bytes);
                // Game.Scene.GetComponent<PlayerComponent>().RealmSession.Send(0, stream);
                // r2CRegiste = (R2C_Registe) await session.Call(new C2R_Registe()
                //     {Account = account, Password = password});
                // FUI_LoadingComponent.HideLoadingUI();
                // if (r2CRegiste.Error == ErrorCode.ERR_AccountHasExist)
                // {
                //     await Game.EventSystem.Publish(new EventType.LoginOrRegsiteFail()
                //         {ErrorMessage = "账号已存在", ZoneScene = zoneScene});
                //     session.Dispose();
                //     return;
                // }
                registerPostfix++;
                await Task.CompletedTask;

                // session.Dispose();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            finally
            {
                FUI_LoadingComponent.HideLoadingUI();
            }
        }
        public static async ETTask Connect(Entity fuiComponent, string address)
        {
            var selectorEndpoint = address!=""? NetworkHelper.ToIPEndPoint(address):NetworkHelper.ToIPEndPoint(CloudIp, port);
            var playerComponent = Game.Scene.GetComponent<PlayerComponent>();
            if (playerComponent.RealmSession==null || playerComponent.RealmSession.IsDisposed)
            {
                // playerComponent.RealmSession = Game.Scene.GetComponent<NetKcpComponent>().Create(selectorEndpoint);
                playerComponent.RealmSession = fuiComponent.DomainScene() .GetComponent<NetKcpComponent>().Create(selectorEndpoint);
            }

            await Task.CompletedTask;
        }

        public static async ETTask ConnectTransfer(string address, int port)
        {
            var lobbySession = fuiComponent.DomainScene()
                .GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(address, port));
            Game.Scene.GetComponent<PlayerComponent>().LobbySession = lobbySession;
            lobbySession.Send(new authenticate_c2s()
            {
                device_product_id = PlayerPrefs.GetString(RegisterHelper. DEVICE_PRODUCT_ID),
                device_type = 1,
                user_id = PlayerPrefs.GetInt(RegisterHelper.USER_ID),
            });
            
            await ETTask.CompletedTask;
        }
    }

    public class Beifen
    {
        public const ushort C2R_Login = 30001;
        public const ushort R2C_Login = 30002;
        public const ushort C2R_Registe = 30003;
        public const ushort R2C_Registe = 30004;
        public const ushort TMsg = 30005;
        public const ushort register_user_c2s = 10000;
        public const ushort register_user_s2c = 10001;
        public const ushort get_transfer_endpoint_c2s = 10002;
        public const ushort get_transfer_endpoint_s2c = 10003;
        public const ushort authenticate_c2s = 20000;
        public const ushort authenticate_s2c = 20001;
        public const ushort heartbeat_c2s = 20002;
        public const ushort all_sync_s2c = 20003;
        public const ushort player_enter_s2c = 20004;
        public const ushort player_leave_s2c = 20005;
        public const ushort action_req_c2s = 20006;
        public const ushort action_req_s2c = 20007;
        public const ushort action_syn_s2c = 20008;
    }
}