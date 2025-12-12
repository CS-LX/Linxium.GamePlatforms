using TapSDK.Achievement;
using UnityEngine;

namespace Linxium.GamePlatforms.TapTap {
    class TapAchievementCallback : ITapAchievementCallback {
        public void OnAchievementSuccess(int code, TapAchievementResult result) {
            Debug.Log($"Achievement success, code: {code}");
        }

        public void OnAchievementFailure(string achievementId, int errorCode, string errorMsg) {
            Debug.LogError($"Achievement failed, code: {errorCode}");
        }
    }
}