using System;
using UnityEngine;

namespace RainbowAssets.BehaviourTree
{
    [CreateAssetMenu(menuName = "New Behaviour Tree")]
    public class BehaviourTree : ScriptableObject
    {
        [SerializeField] Node rootNode;

        public void Bind(BehaviourTreeController controller)
        {
            Traverse(rootNode, node => node.Bind(controller));
        }

        public Status Tick()
        {
            return rootNode.Tick();
        }

        void Traverse(Node node, Action<Node> visiter)
        {
            if (node != null)
            {
                visiter.Invoke(node);

                foreach (var child in node.GetChildren())
                {
                    Traverse(child, visiter);
                }
            }
        }
    }
}