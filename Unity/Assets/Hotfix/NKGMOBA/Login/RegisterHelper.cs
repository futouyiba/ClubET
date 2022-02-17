using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ET
{
    public class RegisterHelper
    { 
        public static string DEVICE_PRODUCT_ID = "DEVICE_PRODUCT_ID";
        public static string DEVICE_MODEL = "DEVICE_MODEL";
        public static string USER_ID = "USER_ID";
        public static int registerPostfix = 40;
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
                Game.Scene.GetComponent<PlayerComponent>()  .RealmSession.Send(new register_user_c2s()
                {
                    device_type = 1,
                    device_model = DEVICE_MODEL + postfixString,
                    device_product_id = DEVICE_PRODUCT_ID + postfixString,
                });
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
                playerComponent.RealmSession = fuiComponent.DomainScene() .GetComponent<NetKcpComponent>().Create(selectorEndpoint);
            }

            await Task.CompletedTask;
        }
    }
}