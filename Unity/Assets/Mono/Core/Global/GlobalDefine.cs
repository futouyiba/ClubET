using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public static class GlobalDefine
    {
        public static bool ILRuntimeMode = true;

        public static bool DevelopMode = true;

        /// <summary>
        /// 固定间隔的目标FPS
        /// </summary>
        public const int FixedUpdateTargetFPS = 30;

        public const float FixedUpdateTargetDTTime = 1f / FixedUpdateTargetFPS;

        private static string s_LoginAddress;

        public static ILog ILog;

        public static Options Options = new Options();

        public static string GetLoginAddress()
        {
            if (GlobalDefine.DevelopMode)
            {
                return "82.157.8.127:8800";
            }
            else
            {
                return s_LoginAddress;
            }
        }

        public static void SetLoginAddress(string loginAddress)
        {
            s_LoginAddress = loginAddress;
        }
    }
}