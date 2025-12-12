namespace Linxium.GamePlatforms {
    public interface IGamePlatformManager {
        GamePlatform Platform { get; }
        bool Initialized { get; }
        void Initialize();
        void UnlockAchievement(string achievementId);
        void ShowAchievements();
        void Uninitialize();
    }
}