using System;
using System.Collections.Generic;
using UnityEngine;

namespace RainbowAssets.BehaviourTree
{
    [CreateAssetMenu(menuName = "New Behaviour Tree")]
    public class BehaviourTree : ScriptableObject
    {
        [SerializeField] Node rootNode;

        public BehaviourTree Clone()
        {
            BehaviourTree clone = Instantiate(this);

            clone.rootNode = rootNode.Clone();

            return clone;
        }

        public void Bind(BehaviourTreeController controller)
        {
            Traverse(rootNode, node => node.Bind(controller));
        }

        public Status Tick()
        {
            return rootNode.Tick();
        }

        IEnumerable<Node> GetAllChildren(Node node)
        {
            CompositeNode compositeNode = node as CompositeNode;

            if (compositeNode != null)
            {
                foreach(var child in compositeNode.GetChildren())
                {
                    yield return child;
                }
            }

            DecoratorNode decoratorNode = node as DecoratorNode;

            if (decoratorNode != null)
            {
                yield return decoratorNode.GetChild();
            }
        }

        void Traverse(Node node, Action<Node> visiter)
        {
            if (node != null)
            {
                visiter.Invoke(node);

                foreach (var child in GetAllChildren(node))
                {
                    Traverse(child, visiter);
                }
            }
        }
    }
}