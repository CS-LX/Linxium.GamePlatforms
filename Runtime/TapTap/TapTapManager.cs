using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Linxium.ExtraComponents;
using TapSDK.Achievement;
using TapSDK.Core;
using TapSDK.Login;
using TapSDK.Update;
using UnityEngine;
using UnityEngine.Scripting;

namespace Linxium.GamePlatforms.TapTap {
public abstract class TapTapManager : AdvancedMonoSingleton<TapTapManager>, IGamePlatformManager {
    public GamePlatform Platform => GamePlatform.TapTap;
    public bool Initialized => isInitialized;

    protected abstract TapTapSdkOptions CoreOptions { get; }
    // 成就配置
    protected virtual TapTapAchievementOptions AchievementOptions { get; } = new() {
        // 成就达成时 SDK 是否需要展示一个气泡弹窗提示
        enableToast = true
    };
    protected virtual ITapAchievementCallback AchievementCallback { get; } = new TapAchievementCallback();
    bool isInitialized = false;

    protected override void OnAwake() {
        base.OnAwake();
        GamePlatformsManager.GetInstance().RegisterPlatform(this);
    }

    public virtual void Initialize() {
        TapTapSDK.Init(CoreOptions, new TapTapSdkBaseOptions[] {AchievementOptions});
        InitializeAsync().Forget();
    }

    [Preserve]
    protected virtual async UniTaskVoid InitializeAsync() {
        isInitialized = false;
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor) {
            bool isSuccess = await TapTapSDK.IsLaunchedFromTapTapPC();
            if (!isSuccess) {
                HandleWarring("[TapTapManager] TapTap PC 端校验未通过");
                return;
            }
        }
        //登录
        // 定义授权范围
        List<string> scopes = new() {
            TapTapLogin.TAP_LOGIN_SCOPE_BASIC_INFO
        };
        try {
            var userInfo = await TapTapLogin.Instance.LoginWithScopes(scopes.ToArray());
        }
        catch (TaskCanceledException taskCanceledException) {
            HandleException(taskCanceledException, "[TapTapManager] 用户取消登录");
            //await MessageDialog.Show("提示", "不登陆会导致无法解锁成就（实际上登录只是为了解锁成就用的喵qwq）");
        }
        catch (Exception exception) {
            HandleException(exception);
            //await MessageDialog.Show("登录失败，出现异常", $"{exception}", MessageDialog.ButtonSet.OK, true);
        }
        // 发起 Tap 登录
        TapTapAchievement.RegisterCallBack(AchievementCallback);
        // 检擦更新
        try {
            TapTapUpdate.CheckForceUpdate();
        }
        catch (Exception e) {
            HandleWarring("[TapTapManager] 检查更新失败" + e);
        }
        isInitialized = true;
    }

    public virtual void HandleWarring(string message) {
        Debug.LogWarning(message);
    }

    public virtual void HandleException(Exception exception, string message = "") {
        if (exception is OperationCanceledException) Debug.LogWarning(message);
        else Debug.LogError(message);
    }

    public virtual void Uninitialize() {
        TapTapAchievement.UnRegisterCallBack(AchievementCallback);
        TapTapLogin.Instance.Logout();
    }

    public virtual void UnlockAchievement(string achievementID) {
        TapTapAchievement.Unlock(achievementID);
    }

    public virtual void ShowAchievements() {
        TapTapAchievement.ShowAchievements();
    }
}
}