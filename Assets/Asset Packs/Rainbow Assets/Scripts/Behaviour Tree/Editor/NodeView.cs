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
        }
    }
}