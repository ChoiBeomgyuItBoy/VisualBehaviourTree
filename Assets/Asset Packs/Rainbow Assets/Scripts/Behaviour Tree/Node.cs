using UnityEngine;

namespace RainbowAssets.BehaviourTree
{
    public abstract class Node : ScriptableObject
    {
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