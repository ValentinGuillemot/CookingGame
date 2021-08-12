using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Character
{
    [SerializeField]
    int basicSpellManaCost = 5;

    [SerializeField]
    int basicSpellDamageBoost = 2;

    [SerializeField]
    int strongSpellManaCost = 20;

    [SerializeField]
    int strongSpellDamageBoost = 10;

    [SerializeField]
    int basicSpellProbability = 2;

    protected override void Attack()
    {
        // Check stronger spells can be used
        if (characterStats.Mana < strongSpellManaCost)
        {
            if (characterStats.Mana >= basicSpellManaCost)
                CastSpell(basicSpellManaCost, basicSpellDamageBoost);
            else
                OnAttack(characterStats.Attack);
            return;
        }

        // Chose which action to use between strong or regular spell
        bool canUseSpecial = (characterStats.Mana >= strongSpellManaCost);
        if (canUseSpecial)
        {
            int randomAction = Random.Range(1, basicSpellProbability + 1);
            canUseSpecial &= (randomAction == 1);
        }

        if (canUseSpecial)
            CastSpell(strongSpellManaCost, strongSpellDamageBoost);
        else
            CastSpell(basicSpellManaCost, basicSpellDamageBoost);
    }

    void CastSpell(int manaCost, int magicBoost)
    {
        int power = characterStats.Magic * magicBoost;
        characterStats.Mana -= manaCost;
        OnAttack(power);
    }
}
