using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace RainbowAssets.BehaviourTree.Editor
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        Node node;
        Port outputPort;
        Port inputPort;

        public NodeView(Node node)
        {
            this.node = node;

            viewDataKey = node.GetUniqueID();
            
            title = node.name;

            style.left = node.GetPosition().x;
            style.top = node.GetPosition().y;

            CreatePorts();
            SetCapabilites();
        }

        public Node GetNode()
        {
            return node;
        }

        public Edge ConnectTo(NodeView child)
        {
            return outputPort.ConnectTo(child.inputPort);
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            node.SetPosition(new Vector2(newPos.x, newPos.y));
        }

        public override void OnSelected()
        {
            base.OnSelected();
            Selection.activeObject = node;
        }

        void SetCapabilites()
        {
            if(node is RootNode)
            {
                capabilities -= Capabilities.Deletable;
            }
        }

        void CreatePorts()
        {
            if(node is not RootNode)
            {
                inputPort = GetPort(Direction.Input, Port.Capacity.Single);
            }

            if(node is DecoratorNode)
            {
                outputPort = GetPort(Direction.Output, Port.Capacity.Single);
            }

            if(node is CompositeNode)
            {
                outputPort = GetPort(Direction.Output, Port.Capacity.Multi);
            }

            inputContainer.Add(inputPort);
            outputContainer.Add(outputPort);
        }

        Port GetPort(Direction direction, Port.Capacity capacity)
        {
            Port newPort = InstantiatePort(Orientation.Vertical, direction, capacity, typeof(bool));

            newPort.portName = "";

            return newPort;
        }
    }
}