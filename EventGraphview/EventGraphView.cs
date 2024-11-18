using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System;
using System.Reflection;
using System.Linq;

namespace UIToolkitDemo
{
    public class EventGraphView : GraphView
    {
        public EventGraphView()
        {
            // 设置缩放和拖拽的行为
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            // 添加背景网格
            var grid = new GridBackground();
            Insert(0, grid);
            grid.StretchToParentSize();
            grid.pickingMode = PickingMode.Ignore; // 确保 GridBackground 不拦截事件

            // 设置视图样式，防止显示异常
            style.flexGrow = 1;

            // 加载样式表
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/EventGraphview/EventGraphView.uss");
            if (styleSheet != null)
            {
                this.styleSheets.Add(styleSheet);
            }

            // 注册拖放事件
            RegisterCallback<DragEnterEvent>(OnDragEnter);
            RegisterCallback<DragLeaveEvent>(OnDragLeave);
            RegisterCallback<DragUpdatedEvent>(OnDragUpdated);
            RegisterCallback<DragPerformEvent>(OnDrop);
        }

        // 拖放进入时高亮显示
        private void OnDragEnter(DragEnterEvent evt)
        {
            if (IsValidDrag(evt))
            {
                AddToClassList("graphview-drop-highlight"); // 添加高亮样式
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy; // 显示复制视觉效果
                evt.StopPropagation();
            }
        }

        // 拖放更新时设置视觉模式
        private void OnDragUpdated(DragUpdatedEvent evt)
        {
            if (IsValidDrag(evt))
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                evt.StopPropagation();
            }
        }

        // 拖放离开时恢复背景
        private void OnDragLeave(DragLeaveEvent evt)
        {
            RemoveFromClassList("graphview-drop-highlight"); // 移除高亮样式
            evt.StopPropagation();
        }

        // 拖放完成时处理文件
        private void OnDrop(DragPerformEvent evt)
        {
            if (IsValidDrag(evt))
            {
                RemoveFromClassList("graphview-drop-highlight"); // 移除高亮样式
                DragAndDrop.AcceptDrag();

                foreach (var path in DragAndDrop.paths)
                {
                    if (path.EndsWith(".cs"))
                    {
                        // 确保路径在 Assets 目录内
                        if (path.StartsWith("Assets/"))
                        {
                            // 使用 AssetDatabase.LoadAssetAtPath 加载 MonoScript，并通过 GetClass 获取脚本类型
                            var script = AssetDatabase.LoadAssetAtPath<MonoScript>(path);
                            if (script != null)
                            {
                                Type type = script.GetClass();
                                if (type != null)
                                {
                                    AddNode(type);
                                }
                                else
                                {
                                    EditorUtility.DisplayDialog("Error", $"无法获取脚本类型: {path}", "OK");
                                }
                            }
                            else
                            {
                                EditorUtility.DisplayDialog("Error", $"无法加载脚本: {path}", "OK");
                            }
                        }
                        else
                        {
                            EditorUtility.DisplayDialog("Error", "请确保拖放的脚本位于 Assets 目录内。", "OK");
                        }
                    }
                    else
                    {
                        EditorUtility.DisplayDialog("Error", "仅支持拖放 .cs 文件。", "OK");
                    }
                }

                evt.StopPropagation();
            }
        }

        // 检查拖放内容是否有效
        private bool IsValidDrag(EventBase evt)
        {
            if (DragAndDrop.paths == null || DragAndDrop.paths.Length == 0)
                return false;

            foreach (var path in DragAndDrop.paths)
            {
                if (!path.EndsWith(".cs") || !path.StartsWith("Assets/"))
                    return false;
            }
            return true;
        }

        // 添加节点到 GraphView
        public void AddNode(Type scriptType)
        {
            // 创建一个新的 EventNode，并将其添加到 GraphView 中
            var node = new EventNode(scriptType);
            // 设置节点的位置，可以根据需要调整
            node.SetPosition(new Rect(new Vector2(UnityEngine.Random.Range(100, 500), UnityEngine.Random.Range(100, 500)), new Vector2(300, 200)));
            AddElement(node);

            // 调试输出
            Debug.Log($"Added Node: {scriptType.Name} at {node.GetPosition().position}");
        }
    }
}