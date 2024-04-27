using UnityEditor;
using UnityEngine;

namespace RainbowAssets.BehaviourTree
{
    public abstract class Node : ScriptableObject
    {
        [SerializeField] Vector2 position;
        Status status = Status.Running;
        bool started = false;
        protected BehaviourTreeController controller;

        public virtual Node Clone()
        {
            return Instantiate(this);
        }

        public void Bind(BehaviourTreeController controller)
        {
            this.controller = controller;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

#if UNITY_EDITOR
        public void SetPosition(Vector2 position)
        {
            Undo.RecordObject(this, "Node Moved");
            this.position = position;
            EditorUtility.SetDirty(this);
        }
#endif

        public Status Tick()
        {
            if (!started)
            {
                OnEnter();
                started = true;
            }

            status = OnTick();

            if (status == Status.Success || status == Status.Failure)
            {
                OnExit();
                started = false;
            }

            return status;
        }

        protected abstract void OnEnter();
        protected abstract Status OnTick();
        protected abstract void OnExit();
    }
}