using UnityEditor;
using UnityEngine;

namespace RainbowAssets.BehaviourTree.Editor
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        Node node;

        public NodeView(Node node)
        {
            this.node = node;
            
            title = node.name;

            style.left = node.GetPosition().x;
            style.top = node.GetPosition().y;
        }

        public Node GetNode()
        {
            return node;
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
    }
}