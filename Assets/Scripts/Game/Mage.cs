using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Character
{
    [SerializeField]
    private int basicSpellManaCost = 5;

    [SerializeField]
    private int basicSpellDamageBoost = 2;

    [SerializeField]
    private int strongSpellManaCost = 20;

    [SerializeField]
    private int strongSpellDamageBoost = 10;

    [SerializeField]
    private int basicSpellProbability = 2;

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

    /// <summary>
	/// Call Attack event with Magic stat
	/// </summary>
	/// <param name="manaCost">MP to remove from the character to use the spell</param>
	/// <param name="magicBoost">Multiply ratio to increase damage given to Attack event</param>
    void CastSpell(int manaCost, int magicBoost)
    {
        int power = characterStats.Magic * magicBoost;
        characterStats.Mana -= manaCost;
        OnAttack(power);
        UpdateUI();
    }
}
