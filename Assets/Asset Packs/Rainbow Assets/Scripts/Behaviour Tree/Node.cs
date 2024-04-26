using System.Collections.Generic;
using UnityEngine;

namespace RainbowAssets.BehaviourTree
{
    public abstract class Node : ScriptableObject
    {
        [SerializeField] List<Node> children = new();
        Status status = Status.Running;
        bool started = false;
        protected BehaviourTreeController controller;

        public void Bind(BehaviourTreeController controller)
        {
            this.controller = controller;
        }

        public List<Node> GetChildren()
        {
            return children;
        }

        public Node GetChild(int index)
        {
            return children[index];
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