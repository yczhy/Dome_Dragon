using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIToolkitDemo
{
    [CreateAssetMenu(fileName = "MediaQueryChannel", menuName = "ScriptableObject/Events/MediaQueryChannel")]
    public class MediaQueryChannel : BaseEventChannelSo
    {

        public event Action<Vector2> ResolutionUpdated;

        public event Action<MediaAspectRatio> AspectRatioUpdated;

        public event Action CameraResized;

        public event Action SafeAreaApplied;


        // 触发 ResolutionUpdated 事件
        public void InvokeResolutionUpdated(Vector2 resolution)
        {
            ResolutionUpdated?.Invoke(resolution);
        }

        // 触发 AspectRatioUpdated 事件
        public void InvokeAspectRatioUpdated(MediaAspectRatio aspectRatio)
        {
            AspectRatioUpdated?.Invoke(aspectRatio);
        }

        // 触发 CameraResized 事件
        public void InvokeCameraResized()
        {
            CameraResized?.Invoke();
        }

        // 触发 SafeAreaApplied 事件
        public void InvokeSafeAreaApplied()
        {
            SafeAreaApplied?.Invoke();
        }
    }
}