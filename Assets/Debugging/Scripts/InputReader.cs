using System;
using RainbowAssets.Utils;
using UnityEngine;

public class InputReader : MonoBehaviour, IPredicateEvaluator
{
    public bool? Evaluate(string predicate, string[] parameters)
    {
        if(predicate == "Input")
        {
            KeyCode keyCode = Enum.Parse<KeyCode>(parameters[0]);

            return Input.GetKeyDown(keyCode);
        }

        return null;
    }
}
