using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultLogistic", menuName = "ScriptableObjects/Function/Logistic", order = 3)]
public class LogisticFunction : Function
{
    [SerializeField]
    float middle = 0.5f;

    [SerializeField]
    float slopeCoef = 10.0f;

    [SerializeField]
    float offset = 0f;

    public override float GetValue(float x)
    {
        float exp = Mathf.Exp(-slopeCoef * (x - middle));
        return Mathf.Clamp((1 / (1 + exp)) + offset, 0f, 1f);
    }
}
