using System;
using System.Collections.Generic;
using UnityEngine;
namespace UIToolkitDemo
{
    [CreateAssetMenu(fileName = "HomeChannel", menuName = "ScriptableObject/Events/HomeChannel")]
    public class HomeChannel : BaseEventChannelSo
    {
        public event Action<string> HomeMessageShown;

        public event Action<LevelSO> levelInfoShown;

        public event Action<List<ChatSO>> ChatWindowShown;

        public event Action MainMenuScreenExited;

        public event Action PlayButtonClicked;

        public event Action HomeScreenEnter;

        public event Action HomeScreenExit;

        // 触发 HomeMessageShown 事件
        public void InvokeHomeMessageShown(string message)
        {
            HomeMessageShown?.Invoke(message);
        }

        // 触发 levelInfoShown 事件
        public void InvokeLevelInfoShown(LevelSO levelInfo)
        {
            levelInfoShown?.Invoke(levelInfo);
        }

        // 触发 ChatWindowShown 事件
        public void InvokeChatWindowShown(List<ChatSO> chatList)
        {
            ChatWindowShown?.Invoke(chatList);
        }

        // 触发 MainMenuScreenExited 事件
        public void InvokeMainMenuScreenExited()
        {
            MainMenuScreenExited?.Invoke();
        }

        // 触发 PlayButtonClicked 事件
        public void InvokePlayButtonClicked()
        {
            PlayButtonClicked?.Invoke();
        }

        // 触发 HomeScreenEnter 事件
        public void InvokeHomeScreenEnter()
        {
            HomeScreenEnter?.Invoke();
        }

        // 触发 HomeScreenExit 事件
        public void InvokeHomeScreenExit()
        {
            HomeScreenExit?.Invoke();
        }
    }
}