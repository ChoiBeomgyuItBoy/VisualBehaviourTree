using System;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

namespace RainbowAssets.BehaviourTree.Editor
{
    public class BehaviourTreeView : GraphView
    {
        new class UxmlFactory : UxmlFactory<BehaviourTreeView, UxmlTraits> { }
        BehaviourTree behaviourTree;

        public BehaviourTreeView()
        {
            StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(BehaviourTreeEditor.path + "BehaviourTreeEditor.uss");
            styleSheets.Add(styleSheet);

            Insert(0, new GridBackground());

            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());

            Undo.undoRedoPerformed += OnUndoRedo;
        }

        public void Refresh(BehaviourTree behaviourTree)
        {
            this.behaviourTree = behaviourTree;

            graphViewChanged -= OnGraphViewChanged;

            DeleteElements(graphElements);

            graphViewChanged += OnGraphViewChanged;

            if(behaviourTree != null)
            {
                foreach(var node in behaviourTree.GetNodes())
                {
                    CreateNodeView(node);
                }
            }
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            if(behaviourTree != null)
            {
                base.BuildContextualMenu(evt);
                
                var nodeTypes = TypeCache.GetTypesDerivedFrom<Node>();

                foreach(var type in nodeTypes)
                {
                    if(!type.IsAbstract)
                    {
                        evt.menu.AppendAction($"Create Node/{type.Name} ({type.BaseType.Name})", a => CreateNode(type));
                    }
                }
            }
        }

        void CreateNodeView(Node node)
        {
            NodeView nodeView = new(node);
            AddElement(nodeView);
        }

        void CreateNode(Type type)
        {
            Node newNode = behaviourTree.CreateNode(type);
            CreateNodeView(newNode);
        }

        void RemoveNode(NodeView nodeView)
        {
            behaviourTree.RemoveNode(nodeView.GetNode());
        }

        GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            var elementsToRemove = graphViewChange.elementsToRemove;

            if(elementsToRemove != null)
            {
                foreach(var element in elementsToRemove)
                {
                    NodeView nodeView = element as NodeView;

                    if(nodeView != null)
                    {
                        RemoveNode(nodeView);
                    }
                }
            }

            return graphViewChange;
        }

        void OnUndoRedo()
        {
            Refresh(behaviourTree);
        }
    }
}