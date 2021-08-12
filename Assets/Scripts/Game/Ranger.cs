using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranger : Character
{
    [SerializeField]
    int specialAttackManaCost = 5;

    [SerializeField]
    int specialAttackBoost = 2;

    [SerializeField]
    int normalAttackProbability = 2;

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
    }
}
