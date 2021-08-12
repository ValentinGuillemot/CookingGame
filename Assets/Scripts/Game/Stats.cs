using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStats", menuName = "ScriptableObjects/Stats", order = 1)]
public class Stats : ScriptableObject
{
    public int Life = 10;

    public int Mana = 10;

    public int Attack = 10;

    public int Magic = 10;

    public int Defense = 10;

    public float Fullness = 10;

    public Stats(Stats copy)
    {
        Life = copy.Life;
        Mana = copy.Mana;
        Attack = copy.Attack;
        Magic = copy.Magic;
        Defense = copy.Defense;
        Fullness = copy.Fullness;
    }

    public static Stats operator +(Stats first, Stats second)
    {
        Stats sumStat = new Stats(first);

        sumStat.Life += second.Life;
        sumStat.Mana += second.Mana;
        sumStat.Attack += second.Attack;
        sumStat.Magic += second.Magic;
        sumStat.Defense += second.Defense;
        sumStat.Fullness += second.Fullness;

        return sumStat;
    }

    public void DisplayStats()
    {
        Debug.Log("Life=" + Life + " ; Mana=" + Mana + " ; Atk=" + Attack + " ; Mag=" + Magic + " ; Def=" + Defense + " ; Full=" + Fullness);
    }

}
