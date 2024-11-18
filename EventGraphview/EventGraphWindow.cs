using System;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace UIToolkitDemo
{
    public class EventGraphWindow : EditorWindow
    {
        private EventGraphView _graphView;

        [MenuItem("Window/Event Graph")]
        public static void OpenWindow()
        {
            var window = GetWindow<EventGraphWindow>();
            window.titleContent = new GUIContent("Event Graph");
        }

        private void OnEnable()
        {
            // 创建 GraphView
            _graphView = new EventGraphView
            {
                name = "Event Graph"
            };
            _graphView.StretchToParentSize();
            rootVisualElement.Add(_graphView);

            // 创建并添加工具栏
            var toolbar = new Toolbar();
            var selectScriptButton = new Button(OnSelectScript) { text = "Select Script" };
            toolbar.Add(selectScriptButton);
            rootVisualElement.Add(toolbar);
        }

        private void OnDisable()
        {
            if (_graphView != null)
            {
                rootVisualElement.Remove(_graphView);
            }
        }

        private void OnSelectScript()
        {
            // 打开选择脚本的窗口
            string path = EditorUtility.OpenFilePanel("Select Script", Application.dataPath, "cs");
            if (!string.IsNullOrEmpty(path))
            {
                // 转换为相对路径
                if (path.StartsWith(Application.dataPath))
                {
                    path = "Assets" + path.Substring(Application.dataPath.Length);
                }

                // 获取类型
                var script = AssetDatabase.LoadAssetAtPath<MonoScript>(path);
                if (script != null)
                {
                    Type type = script.GetClass();
                    if (type != null)
                    {
                        // 清空现有节点
                        _graphView.DeleteElements(_graphView.nodes.ToList());

                        // 添加节点
                        _graphView.AddNode(type);
                    }
                    else
                    {
                        EditorUtility.DisplayDialog("Error", "无法获取脚本的类型。", "OK");
                    }
                }
                else
                {
                    EditorUtility.DisplayDialog("Error", "无法加载脚本。", "OK");
                }
            }
        }
    }
}