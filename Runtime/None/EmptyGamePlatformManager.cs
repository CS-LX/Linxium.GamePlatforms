using Linxium.ExtraComponents;
using UnityEngine;

namespace Linxium.GamePlatforms.None {
    [DisallowMultipleComponent]
    public class EmptyGamePlatformManager : AdvancedMonoSingleton<EmptyGamePlatformManager>, IGamePlatformManager {
        public GamePlatform Platform => GamePlatform.None;
        public bool Initialized => true;

        protected override void OnAwake() {
            GamePlatformsManager.GetInstance().RegisterPlatform(this);
        }
        public void Initialize() {
            print(nameof(EmptyGamePlatformManager) + " initialized");
        }
        public void UnlockAchievement(string achievementId) {
            print(nameof(EmptyGamePlatformManager) + " unlock achievement " + achievementId);
        }
        public void ShowAchievements() {
            print(nameof(EmptyGamePlatformManager) + " show achievements");
        }
        public void Uninitialize() {
        }
    }
}