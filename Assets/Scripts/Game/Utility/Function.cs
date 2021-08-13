using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Function : ScriptableObject
{
    public abstract float GetValue(float x);
}
