using System.Linq;

namespace RainbowAssets.BehaviourTree.Composites
{
    public class Selector : CompositeNode
    {
        int currentChildIndex = 0;

        protected override void OnEnter()
        {
            currentChildIndex = 0;
        }

        protected override Status OnTick()
        {
            Status currentStatus = GetChild(currentChildIndex).Tick();

            switch (currentStatus)
            {
                case Status.Running:
                    return Status.Running;

                case Status.Success:
                    return Status.Success;

                case Status.Failure:
                    currentChildIndex++;
                    break;
            }

            if (currentChildIndex >= GetChildren().Count())
            {
                return Status.Failure;
            }

            return Status.Running;
        }

        protected override void OnExit() { }
    }
}