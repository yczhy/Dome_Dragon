using System;
using UnityEngine;

namespace UIToolkitDemo
{
    [CreateAssetMenu(fileName = "MainMenuUIChannel", menuName = "ScriptableObject/Events/MainMenuUIChannel")]
    public class MainMenuUIChannel : BaseEventChannelSo
    {
        public event Action HomeScreenShown;

        public event Action CharScreenShown;

        public event Action InfoScreenShown;

        public event Action ShopScreenShown;

        public event Action OptionsBarShopScreenShown;

        public event Action MailScreenShown;

        public event Action SettingsScreenShown;

        public event Action InventoryScreenShown;

        public event Action SettingsScreenHidden;

        public event Action InventoryScreenHidden;

        public event Action GameScreenShown;

        public event Action<MenuScreen> CurrentScreenChanged;

        public event Action<string> CurrentViewChanged;

        public event Action<string> TabbedUIReset;

        // 触发 HomeScreenShown 事件
        public void InvokeHomeScreenShown()
        {
            HomeScreenShown?.Invoke();
        }

        // 触发 CharScreenShown 事件
        public void InvokeCharScreenShown()
        {
            CharScreenShown?.Invoke();
        }

        // 触发 InfoScreenShown 事件
        public void InvokeInfoScreenShown()
        {
            InfoScreenShown?.Invoke();
        }

        // 触发 ShopScreenShown 事件
        public void InvokeShopScreenShown()
        {
            ShopScreenShown?.Invoke();
        }

        // 触发 OptionsBarShopScreenShown 事件
        public void InvokeOptionsBarShopScreenShown()
        {
            OptionsBarShopScreenShown?.Invoke();
        }

        // 触发 MailScreenShown 事件
        public void InvokeMailScreenShown()
        {
            MailScreenShown?.Invoke();
        }

        // 触发 SettingsScreenShown 事件
        public void InvokeSettingsScreenShown()
        {
            SettingsScreenShown?.Invoke();
        }

        // 触发 InventoryScreenShown 事件
        public void InvokeInventoryScreenShown()
        {
            InventoryScreenShown?.Invoke();
        }

        // 触发 SettingsScreenHidden 事件
        public void InvokeSettingsScreenHidden()
        {
            SettingsScreenHidden?.Invoke();
        }

        // 触发 InventoryScreenHidden 事件
        public void InvokeInventoryScreenHidden()
        {
            InventoryScreenHidden?.Invoke();
        }

        // 触发 GameScreenShown 事件
        public void InvokeGameScreenShown()
        {
            GameScreenShown?.Invoke();
        }

        // 触发 CurrentScreenChanged 事件
        public void InvokeCurrentScreenChanged(MenuScreen menuScreen)
        {
            CurrentScreenChanged?.Invoke(menuScreen);
        }

        // 触发 CurrentViewChanged 事件
        public void InvokeCurrentViewChanged(string viewName)
        {
            CurrentViewChanged?.Invoke(viewName);
        }

        // 触发 TabbedUIReset 事件
        public void InvokeTabbedUIReset(string tabName)
        {
            TabbedUIReset?.Invoke(tabName);
        }
    }
}