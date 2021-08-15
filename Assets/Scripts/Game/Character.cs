using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Character : MonoBehaviour
{
    [SerializeField]
    protected Stats characterStats;

    protected int maxHP;
    protected int maxMP;
    private float maxFullness;
    private bool canEat = true;
    private bool willEat = false;
    private bool hasEaten = false;
    private float digestSpeed = 2f;

    [SerializeField]
    private XRSocketCustom plate;

    // Attack event
    public delegate void AttackEvent(int strength);
    public AttackEvent OnAttack;

    [SerializeField]
    private float timeBetweenAttacks = 3f;
    private float currentTime;

    [SerializeField]
    protected List<Utility> utilities;

    [SerializeField]
    private float timeBetweenUtilitiesCheck = 5f;
    private float utilityTime = 0f;

    // UI
    [SerializeField]
    private SpriteRenderer healthBar;
    [SerializeField]
    private SpriteRenderer manaBar;
    [SerializeField]
    private TextMeshPro attackDisplay;
    [SerializeField]
    private TextMeshPro magicDisplay;
    [SerializeField]
    private TextMeshPro defenseDisplay;
    [SerializeField]
    private SpriteRenderer fullnessBar;

    void Start()
    {
        maxHP = characterStats.Life;
        maxMP = characterStats.Mana;
        maxFullness = characterStats.Fullness;

        // Runtime stats are created as a copy of the initial default stats
        characterStats = Instantiate<Stats>(characterStats);
        characterStats.Fullness = 0;

        currentTime = timeBetweenAttacks;
        utilityTime = timeBetweenUtilitiesCheck;

        plate.onSelectFood += EatFood;

        UpdateUI();
    }

    void Update()
    {
        if (IsDead())
            return;

        Digest();

        currentTime -= Time.deltaTime;
        if (currentTime <= 0f)
        {
            Attack();
            currentTime = timeBetweenAttacks;
        }

        // Run utility system
        if (canEat)
        {
            utilityTime -= Time.deltaTime;
            if (utilityTime <= 0f)
            {
                AskForBoost();
                utilityTime = timeBetweenUtilitiesCheck;
            }
        }

        // Must be done in update outside of EatFood function to prevent errors in XRInteractionManager
        if (hasEaten)
        {
            plate.RemoveFood();
            hasEaten = false;
        }
    }

    /// <summary>
    /// Receive stats bonus from food currently stored in plate
    /// </summary>
    void EatFood()
    {
        if (IsDead())
            return;

        if (!canEat)
        {
            willEat = true;
            return;
        }

        if (plate.HasSelectedItem())
        {
            Food toEat = plate.GetFoodFromPlate();
            if (toEat)
            {
                characterStats += toEat.StatBoost;
                CheckMax();
                UpdateUI();
                hasEaten = true;
            }
        }
    }

    /// <summary>
    /// Gradually reduce fullness over time
    /// </summary>
    void Digest()
    {
        if (characterStats.Fullness == 0)
            return;

        // If fullness reaches 0, character can eat again
        characterStats.Fullness = Mathf.Max(0, characterStats.Fullness - Time.deltaTime * digestSpeed);
        if (!canEat && characterStats.Fullness == 0)
        {
            fullnessBar.color = Color.yellow;
            canEat = true;
            if (willEat)
            {
                willEat = false;
                EatFood();
            }
        }

        // Update fullness gauge
        float fullnessRatio = Mathf.Clamp(characterStats.Fullness / maxFullness, 0f, 1f);
        fullnessBar.transform.parent.localScale = new Vector3(3 * fullnessRatio, 0.5f, 1f);
    }

    /// <summary>
    /// Utility system to ask for specific boost
    /// </summary>
    protected virtual void AskForBoost()
    {
        if (utilities.Count <= 0)
            return;

        int bestUtilityId = 0;
        float bestUtility = 0f;

        // Loop through all utilities to find most needed one
        for (int i = 0; i < utilities.Count; i++)
        {
            float currentUtility = utilities[i].GetUtility(characterStats, maxHP, maxMP);
            if (currentUtility > bestUtility)
            {
                bestUtility = currentUtility;
                bestUtilityId = i;
            }
        }

        utilities[bestUtilityId].DoAction();
    }

    /// <summary>
    /// Call Attack event
    /// </summary>
    protected virtual void Attack()
    {
        OnAttack(characterStats.Attack);
    }

    /// <summary>
    /// Clamp current stats inside gauges (HP, MP, Fullness)
    /// </summary>
    void CheckMax()
    {
        if (characterStats.Life > maxHP)
            characterStats.Life = maxHP;

        if (characterStats.Mana > maxMP)
            characterStats.Mana = maxMP;

        if (characterStats.Fullness > maxFullness)
        {
            if (fullnessBar)
                fullnessBar.color = Color.red;
            canEat = false;
        }
    }

    /// <summary>
    /// Reduce HP depending on defense and input strength
    /// </summary>
    /// /// <param name="strength">damage to deal to the character (reduced by defense stat)</param>
    public void TakeDamage(int strength)
    {
        int damage = Mathf.Max(1, strength - characterStats.Defense);
        characterStats.Life -= damage;

        if (!IsDead())
            UpdateUI();
    }

    /// <summary>
    /// Return true if HP have reached 0
    /// </summary>
    public bool IsDead()
    {
        return (characterStats.Life <= 0);
    }

    /// <summary>
    /// Update UI with current stats
    /// </summary>
    protected void UpdateUI()
    {
        healthBar.transform.parent.localScale = new Vector3(3 * (float)characterStats.Life / (float)maxHP, 0.5f, 1f);
        manaBar.transform.parent.localScale = new Vector3(3 * (float)characterStats.Mana / (float)maxMP, 0.5f, 1f);
        attackDisplay.text = "Attack: " + characterStats.Attack;
        magicDisplay.text = "Magic: " + characterStats.Magic;
        defenseDisplay.text = "Defense: " + characterStats.Defense;
        float fullnessRatio = Mathf.Clamp(characterStats.Fullness / maxFullness, 0f, 1f);
        fullnessBar.transform.parent.localScale = new Vector3(3 * fullnessRatio, 0.5f, 1f);
    }
}
