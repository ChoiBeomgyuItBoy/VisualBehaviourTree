using UnityEngine;
using RainbowAssets.Utils;

public class Patroller : MonoBehaviour, IAction, IPredicateEvaluator
{
    [SerializeField] PatrolPath patrolPath;
    [SerializeField] [Range(0,1)] float patrolSpeedFraction = 0.6f;
    Mover mover;
    int currentWaypointIndex = 0;

    void Awake()
    {
        mover = GetComponent<Mover>();
    }

    void MoveToWaypoint()
    {
        mover.MoveTo(GetCurrentWaypoint(), patrolSpeedFraction);
    }

    void CycleWaypoint()
    {
        currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
    }

    Vector3 GetCurrentWaypoint()
    {
        return patrolPath.GetWaypoint(currentWaypointIndex);
    }

    bool AtWaypoint()
    {
        return mover.AtDestination(GetCurrentWaypoint());
    }

    void IAction.DoAction(string actionID, string[] parameters)
    {
        if (actionID == "Move To Waypoint")
        {
            MoveToWaypoint();
        }

        if (actionID == "Cycle Waypoint")
        {
            CycleWaypoint();
        }
    }

    bool? IPredicateEvaluator.Evaluate(string predicate, string[] parameters)
    {
        if (predicate == "At Waypoint")
        {
            return AtWaypoint();
        }

        return null;
    }
}
