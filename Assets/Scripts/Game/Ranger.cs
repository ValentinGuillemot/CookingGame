using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranger : Character
{
    [SerializeField]
    private int specialAttackManaCost = 5;

    [SerializeField]
    private int specialAttackBoost = 2;

    [SerializeField]
    private int normalAttackProbability = 2;

    // Specific utility to store an action when attack is higher than magic
    [SerializeField]
    private Utility alternateUtility;

    protected override void Attack()
    {
        bool canUseSpecial = (characterStats.Mana >= specialAttackManaCost);
        if (canUseSpecial)
        {
            int randomAction = Random.Range(1, normalAttackProbability + 1);
            canUseSpecial &= (randomAction == 1);
        }

        if (canUseSpecial)
            RangerSpecial();
        else
            OnAttack(characterStats.Attack);
    }

    void RangerSpecial()
    {
        int power = characterStats.Magic * specialAttackBoost;
        characterStats.Mana -= specialAttackManaCost;
        OnAttack(power);
        UpdateUI();
    }

    protected override void AskForBoost()
    {
        if (utilities.Count <= 0)
            return;

        int bestUtilityId = 0;
        float bestUtility = 0f;

        for (int i = 0; i < utilities.Count; i++)
        {
            float currentUtility = utilities[i].GetUtility(characterStats, maxHP, maxMP);
            if (currentUtility > bestUtility)
            {
                bestUtility = currentUtility;
                bestUtilityId = i;
            }
        }

        if (utilities[bestUtilityId].Stat == Stats.EStatType.Attack && characterStats.Attack > characterStats.Magic)
            alternateUtility.DoAction();
        else
            utilities[bestUtilityId].DoAction();
    }
}
