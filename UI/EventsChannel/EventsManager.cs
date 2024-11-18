using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

namespace UIToolkitDemo
{
    [CreateAssetMenu(fileName = "EventsManager", menuName = "ScriptableObject/Events/EventsManager")]
    public class EventsManager : ScriptableObject
    {

        [SerializeField]
        private List<BaseEventChannelSo> EventsList;

        void OnEnable()
        {
            InitEventsList();
        }

        private void InitEventsList()
        {
            EventsList = new List<BaseEventChannelSo>();
            EventsList.AddRange(Resources.LoadAll<BaseEventChannelSo>("GameData/Events"));
            Debug.Log($"EventManager initialized with {EventsList.Count} event channels.");
        }

        // 查询 DIC 中的指定事件通道
        public T GetEventChannelsByType<T>() where T : BaseEventChannelSo
        {
            if (EventsList == null) 
            {
                Debug.Log("EventsList is null");
                return null;
            }

            var matchingChannels = EventsList.OfType<T>().ToList();

            if (matchingChannels.Count == 1)
            {
                return matchingChannels[0];
            }
            else if (matchingChannels.Count == 0 || matchingChannels.Count > 1)
            {
                Debug.Log($"Multiple event channels of type {typeof(T).Name} found.");
            }
            return null;
        }

        void OnDisable()
        {
            EventsList.Clear();
        }

    }
}
