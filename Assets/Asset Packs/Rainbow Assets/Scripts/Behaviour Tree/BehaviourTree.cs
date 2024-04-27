using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RainbowAssets.BehaviourTree
{
    [CreateAssetMenu(menuName = "New Behaviour Tree")]
    public class BehaviourTree : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] Node rootNode;
        [SerializeField] List<Node> nodes = new();

        public BehaviourTree Clone()
        {
            BehaviourTree clone = Instantiate(this);

            clone.rootNode = rootNode.Clone();
            clone.nodes.Clear();

            Traverse(clone.rootNode, node => clone.nodes.Add(node));

            return clone;
        }

        public void Bind(BehaviourTreeController controller)
        {
            Traverse(rootNode, node => node.Bind(controller));
        }

        public IEnumerable<Node> GetNodes()
        {
            return nodes;
        }

        public Status Tick()
        {
            return rootNode.Tick();
        }

        public Node CreateNode(Type type)
        {
            Node newNode = CreateInstance(type) as Node;
            newNode.name = type.Name;
            nodes.Add(newNode);
            return newNode;
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            if(AssetDatabase.GetAssetPath(this) != "")
            {
                foreach(var node in nodes)
                {
                    if(AssetDatabase.GetAssetPath(node) == "")
                    {
                        AssetDatabase.AddObjectToAsset(node, this);
                    }
                }
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize() { }

        IEnumerable<Node> GetChildren(Node node)
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

                foreach (var child in GetChildren(node))
                {
                    Traverse(child, visiter);
                }
            }
        }
    }
}