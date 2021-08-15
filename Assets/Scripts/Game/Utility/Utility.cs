using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Utility
{
    [SerializeField]
    private string actionText;

    [SerializeField]
    private Stats.EStatType statToCheck;

    public Stats.EStatType Stat
    {
        get { return statToCheck; }
    }

    [SerializeField]
    private Function function;

    /// <summary>
	/// Get utility depending on stat to check and used function
	/// </summary>
	/// <param name="givenStats">Stats to check values from</param>
	/// <param name="maxHP">Max HP needed to make ratio with stats HP</param>
	/// <param name="maxMP">Max MP needed to make ratio with stats MP</param>
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

    /// <summary>
	/// Display as a Debug Log the action stored in the utility
	/// </summary>
    public void DoAction()
    {
        Debug.Log(actionText);
    }
}
