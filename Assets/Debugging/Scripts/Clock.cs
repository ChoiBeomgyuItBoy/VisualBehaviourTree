using UnityEngine;
using RainbowAssets.Utils;

public class Clock : MonoBehaviour, IAction, IPredicateEvaluator
{
    [SerializeField] float localTime = 0;

    void IAction.DoAction(string actionID, string[] parameters)
    {
        if(actionID == "Update Local Time")
        {
            localTime += Time.deltaTime;
        }

        if(actionID == "Reset Local Time")
        {
            localTime = 0;
        }
    }

    bool? IPredicateEvaluator.Evaluate(string predicate, string[] parameters)
    {
        if(predicate == "Has Time")
        {
            return localTime >= float.Parse(parameters[0]);
        }

        return null;
    }
}
