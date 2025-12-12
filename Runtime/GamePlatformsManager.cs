using System;
using System.Collections.Generic;
using Linxium.ExtraComponents;

namespace Linxium.GamePlatforms {
    public class GamePlatformsManager : AdvancedMonoSingleton<GamePlatformsManager> {
        public GamePlatform currentPlatform;
        public IGamePlatformManager CurrentPlatformManager { get; private set; }

        Dictionary<GamePlatform, IGamePlatformManager> platformManagers = new();

        public void RegisterPlatform(IGamePlatformManager platformManager) {
            platformManagers.Add(platformManager.Platform, platformManager);
        }

        protected override void OnStart() {
            base.OnStart();
            if (platformManagers.TryGetValue(currentPlatform, out IGamePlatformManager platformManager)) {
                platformManager.Initialize();
                CurrentPlatformManager = platformManager;
            }
        }

        public void UnlockAchievement(string achievementId) {
            if (CurrentPlatformManager is not { Initialized: true }) {
                throw new InvalidOperationException("Current platform manager is not initialized.");
            }

            CurrentPlatformManager.UnlockAchievement(achievementId);
        }

        protected override void OnDispose() {
            base.OnDispose();
            CurrentPlatformManager.Uninitialize();
            CurrentPlatformManager = null;
        }
    }
}