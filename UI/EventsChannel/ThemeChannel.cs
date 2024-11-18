using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIToolkitDemo
{
    [CreateAssetMenu(fileName = "ThemeChannel", menuName = "ScriptableObject/Events/ThemeChannel")]
    public class ThemeChannel : BaseEventChannelSo
    {
        public Action<string> ThemeChanged;

        public Action<Camera> CameraUpdated;

        // 触发 ThemeChanged 事件
        public void InvokeThemeChanged(string themeName)
        {
            ThemeChanged?.Invoke(themeName);
        }

        // 触发 CameraUpdated 事件
        public void InvokeCameraUpdated(Camera camera)
        {
            CameraUpdated?.Invoke(camera);
        }
    }
}