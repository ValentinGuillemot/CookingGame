using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultStep", menuName = "ScriptableObjects/Function/Step", order = 1)]
public class StepFunction : Function
{
    [SerializeField]
    private float step = 0.5f;

    [SerializeField]
    private float valueUnderStep = 0.0f;

    [SerializeField]
    private float valueAboveStep = 1.0f;

    public override float GetValue(float x)
    {
        float value = (x < step) ? valueUnderStep : valueAboveStep;
        return Mathf.Clamp(value, 0f, 1f);
    }
}
