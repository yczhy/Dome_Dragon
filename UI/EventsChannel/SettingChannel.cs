using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIToolkitDemo
{
    [CreateAssetMenu(fileName = "SettingChannel", menuName = "ScriptableObject/Events/SettingChannel")]
    public class SettingChannel : BaseEventChannelSo
    {
        public event Action PlayerFundsResetEvent; // 玩家资金重置

        public event Action PlayerLevelResetEvent; // 玩家等级重置

        public event Action SettingsShownEvent; // 设置界面显示

        public event Action<string> ThemeSelectedEvent; // 主题选择

        public event Action<GameData> GameDataLoadedEvent; // 游戏数据加载

        public event Action<GameData> UIGameDataUpdatedEvent; // UI游戏数据更新

        public event Action<GameData> SettingsUpdatedEvent; // 设置更新

        public event Action<bool> FpsCounterToggledEvent; // FPS计数器开关

        public event Action<int> TargetFrameRateSetEvent; // 目标帧率设置

        public void InvokePlayerFundsReset() => PlayerFundsResetEvent?.Invoke();
        public void InvokePlayerLevelReset() => PlayerLevelResetEvent?.Invoke();
        public void InvokeSettingsShown() => SettingsShownEvent?.Invoke();
        public void InvokeThemeSelected(string theme) => ThemeSelectedEvent?.Invoke(theme);
        public void InvokeGameDataLoaded(GameData data) => GameDataLoadedEvent?.Invoke(data);
        public void InvokeUIGameDataUpdated(GameData data) => UIGameDataUpdatedEvent?.Invoke(data);
        public void InvokeSettingsUpdated(GameData data) => SettingsUpdatedEvent?.Invoke(data);
        public void InvokeFpsCounterToggled(bool state) => FpsCounterToggledEvent?.Invoke(state);
        public void InvokeTargetFrameRateSet(int frameRate) => TargetFrameRateSetEvent?.Invoke(frameRate);
    }
}