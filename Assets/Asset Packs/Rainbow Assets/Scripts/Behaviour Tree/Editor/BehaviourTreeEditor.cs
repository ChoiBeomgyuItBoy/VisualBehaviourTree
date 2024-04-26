using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine.UIElements;

namespace RainbowAssets.BehaviourTree.Editor
{
    public class BehaviourTreeEditor : EditorWindow
    {
        public const string path = "Assets/Asset Packs/Rainbow Assets/Scripts/Behaviour Tree/Editor/";

        [MenuItem("Rainbow Assets/Behaviour Tree Editor")]
        public static void ShowWindow()
        {
            GetWindow(typeof(BehaviourTreeEditor), false, "Behaviour Tree Editor");
        }

        [OnOpenAsset]
        public static bool OnBehaviourTreeOpened(int instanceID, int line)
        {
            BehaviourTree behaviourTree = EditorUtility.InstanceIDToObject(instanceID) as BehaviourTree;

            if(behaviourTree != null)
            {
                ShowWindow();
                return true;
            }

            return false;
        }

        void CreateGUI()
        {
            VisualElement root = rootVisualElement;

            VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path + "BehaviourTreeEditor.uxml");
            visualTree.CloneTree(root);
        }
    }
}