using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.Reflection;
using System.Linq;

namespace UIToolkitDemo
{
    public class EventNode : Node
    {
        private Type _scriptType;
        private Foldout _variablesFoldout;
        private Foldout _methodsFoldout;

        public EventNode(Type scriptType)
        {
            _scriptType = scriptType;
            title = _scriptType.Name;

            // 输入和输出端口（整体节点的入口和出口）
            var input = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
            input.portName = "In";
            input.portColor = Color.green;
            inputContainer.Add(input);

            var output = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
            output.portName = "Out";
            output.portColor = Color.red;
            outputContainer.Add(output);

            // 创建变量和方法的Foldout
            CreateFoldouts();

            // 重新计算和刷新节点的布局与端口状态
            RefreshExpandedState();
            RefreshPorts();
        }

        private void CreateFoldouts()
        {
            // 获取仅在当前类型中声明的字段和方法
            var fields = _scriptType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly);
            var methods = _scriptType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly)
                                     .Where(m => !m.IsSpecialName && !m.IsConstructor);

            // Variables Foldout
            _variablesFoldout = new Foldout { text = "Variables" };
            foreach (var field in fields)
            {
                var label = new Label($"{field.FieldType.Name} {field.Name}");
                _variablesFoldout.Add(label);
            }
            mainContainer.Add(_variablesFoldout);

            // Methods Foldout
            _methodsFoldout = new Foldout { text = "Methods" };
            foreach (var method in methods)
            {
                // 创建方法的水平布局
                var methodRow = new VisualElement();
                methodRow.AddToClassList("method-row"); // 添加样式类

                // 左侧输入端口
                var inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(float));
                inputPort.portName = ""; // 隐藏端口名称
                inputPort.portColor = Color.yellow;
                methodRow.Add(inputPort);

                // 方法名标签
                var parameters = method.GetParameters();
                string paramString = string.Join(", ", Array.ConvertAll(parameters, p => $"{p.ParameterType.Name} {p.Name}"));
                var labelText = $"{method.ReturnType.Name} {method.Name}({paramString})";
                var methodLabel = new Label(labelText);
                methodLabel.AddToClassList("method-label"); // 添加样式类
                methodRow.Add(methodLabel);

                // 右侧输出端口
                var outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
                outputPort.portName = ""; // 隐藏端口名称
                outputPort.portColor = Color.blue;
                methodRow.Add(outputPort);

                _methodsFoldout.Add(methodRow);
            }
            mainContainer.Add(_methodsFoldout);
        }
    }
}