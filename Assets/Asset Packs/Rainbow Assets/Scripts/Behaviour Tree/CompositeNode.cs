using System.Collections.Generic;
using UnityEngine;

namespace RainbowAssets.BehaviourTree
{
    public abstract class CompositeNode : Node
    {
        [SerializeField] List<Node> children = new();

        public IEnumerable<Node> GetChildren()
        {
            return children;
        }

        public Node GetChild(int index)
        {
            return children[index];
        }
    }
}