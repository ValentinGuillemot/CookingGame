using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Character
{
    [SerializeField]
    private int specialAttackManaCost = 10;

    [SerializeField]
    private int specialAttackDefenseCost = 5;

    [SerializeField]
    private int normalAttackProbability = 2;

    protected override void Attack()
    {
        bool canUseSpecial = (characterStats.Mana >= specialAttackManaCost && characterStats.Defense > specialAttackDefenseCost);
        if (canUseSpecial)
        {
            int randomAction = Random.Range(1, normalAttackProbability + 1);
            canUseSpecial &= (randomAction == 1);
        }

        if (canUseSpecial)
            WarriorSpecial();
        else
            OnAttack(characterStats.Attack);
    }

    void WarriorSpecial()
    {
        int attack = characterStats.Attack * 2;
        characterStats.Mana -= specialAttackManaCost;
        characterStats.Defense -= specialAttackDefenseCost;
        OnAttack(attack);
        UpdateUI();
    }
}
