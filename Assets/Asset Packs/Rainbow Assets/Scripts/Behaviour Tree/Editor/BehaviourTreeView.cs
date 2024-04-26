using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

namespace RainbowAssets.BehaviourTree.Editor
{
    public class BehaviourTreeView : GraphView
    {
        new class UxmlFactory : UxmlFactory<BehaviourTreeView, UxmlTraits> { }

        public BehaviourTreeView()
        {
            StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(BehaviourTreeEditor.path + "BehaviourTreeEditor.uss");
            styleSheets.Add(styleSheet);

            Insert(0, new GridBackground());

            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
        }
    }
}