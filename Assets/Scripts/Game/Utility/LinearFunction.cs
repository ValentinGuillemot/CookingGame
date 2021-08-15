using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultLinear", menuName = "ScriptableObjects/Function/Linear", order = 2)]
public class LinearFunction : Function
{
    [SerializeField]
    private float slope = 1.0f;

    // y value for x = 0
    [SerializeField]
    private float offset = 0.0f;

    public override float GetValue(float x)
    {
        return Mathf.Clamp(slope * x + offset, 0f, 1f);
    }
}
