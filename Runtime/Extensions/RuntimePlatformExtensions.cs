using UnityEngine;

namespace Linxium.GamePlatforms.Extensions {
    public static class RuntimePlatformExtensions {
        /// <summary>
        /// 是否为编辑器
        /// </summary>
        public static bool IsEditor(this RuntimePlatform platform) {
            return platform switch {
                RuntimePlatform.OSXEditor => true,
                RuntimePlatform.LinuxEditor => true,
                RuntimePlatform.WindowsEditor => true,
                _ => false
            };
        }

        /// <summary>
        /// 是否为PC平台
        /// </summary>
        public static bool IsPC(this RuntimePlatform platform) {
            return platform switch {
                RuntimePlatform.WindowsPlayer => true,
                RuntimePlatform.LinuxPlayer => true,
                RuntimePlatform.OSXPlayer => true,
                RuntimePlatform.WindowsEditor => true,
                RuntimePlatform.LinuxEditor => true,
                RuntimePlatform.OSXEditor => true,
                _ => false
            };
        }

        /// <summary>
        /// 是否为移动平台
        /// </summary>
        public static bool IsMobile(this RuntimePlatform platform) {
            return platform switch {
                RuntimePlatform.IPhonePlayer => true,
                RuntimePlatform.Android => true,
                RuntimePlatform.tvOS => true,
                RuntimePlatform.Switch => true,
                _ => false
            };
        }

        /// <summary>
        /// 是否为游戏主机平台
        /// </summary>
        public static bool IsConsole(this RuntimePlatform platform) {
            return platform switch {
                RuntimePlatform.PS4 => true,
                RuntimePlatform.XboxOne => true,
                RuntimePlatform.PS5 => true,
                RuntimePlatform.Switch => true,
                _ => false
            };
        }

        /// <summary>
        /// 是否为Web平台
        /// </summary>
        public static bool IsWeb(this RuntimePlatform platform) {
            return platform switch {
                RuntimePlatform.WebGLPlayer => true,
                _ => false
            };
        }

        /// <summary>
        /// 是否为服务器平台
        /// </summary>
        public static bool IsServer(this RuntimePlatform platform) {
            return platform switch {
                RuntimePlatform.LinuxServer => true,
                RuntimePlatform.WindowsServer => true,
                RuntimePlatform.OSXServer => true,
                _ => false
            };
        }
    }
}
