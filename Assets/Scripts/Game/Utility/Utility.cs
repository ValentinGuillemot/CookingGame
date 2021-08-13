using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Utility
{
    [SerializeField]
    string actionText;

    [SerializeField]
    Stats.EStatType statToCheck;

    public Stats.EStatType Stat
    {
        get { return statToCheck; }
    }

    [SerializeField]
    Function function;

    // Max values added to make HP and MP percentage
    public float GetUtility(Stats givenStats, int maxHP = 0, int maxMP = 0)
    {
        float neededStat = 0f;

        switch (statToCheck)
        {
            case Stats.EStatType.Life:
                neededStat = 1f - ((float)givenStats.Life / (float)maxHP); // Percentage of lost life
                break;
            case Stats.EStatType.Mana:
                neededStat = 1f - ((float)givenStats.Mana / (float)maxMP); // Percentage of lost mana
                break;
            case Stats.EStatType.Attack:
                neededStat = (float)givenStats.Attack;
                break;
            case Stats.EStatType.Magic:
                neededStat = (float)givenStats.Magic;
                break;
            case Stats.EStatType.Defense:
                neededStat = (float)givenStats.Defense;
                break;
            default:return 0f;
        }

        return function.GetValue(neededStat);
    }

    public void DoAction()
    {
        Debug.Log(actionText);
    }
}
