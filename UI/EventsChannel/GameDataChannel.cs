using UnityEngine;
using System;

namespace UIToolkitDemo
{
    [CreateAssetMenu(fileName = "GameDataChannel", menuName = "ScriptableObject/Events/GameDataChannel")]
    public class GameDataChannel : BaseEventChannelSo
    {
        public event Action<GameData> GameDataLoadedEvent;

        // 触发 GameDataLoaded 事件
        public void InvokeGameDataLoaded(GameData data)  
        {
            GameDataLoadedEvent?.Invoke(data);
        }
    }
}

