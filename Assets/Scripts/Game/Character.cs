using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    XRSocketCustom plate;

    // Attack event
    public delegate void AttackEvent(int strength);
    public AttackEvent OnAttack;

    [SerializeField]
    float timeBetweenAttacks = 3f;
    float currentTime;

    [SerializeField]
    protected List<Utility> utilities;

    [SerializeField]
    float timeBetweenUtilitiesCheck = 5f;
    float utilityTime = 0f;

    // Start is called before the first frame update
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
    }

    // Update is called once per frame
    void Update()
    {
        Digest();

        currentTime -= Time.deltaTime;
        if (currentTime <= 0f)
        {
            Attack();
            currentTime = timeBetweenAttacks;
        }

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

    void EatFood()
    {
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
                hasEaten = true;
            }
        }
    }

    void Digest()
    {
        // If fullness reaches 0, character can eat again
        characterStats.Fullness = Mathf.Max(0, characterStats.Fullness - Time.deltaTime * digestSpeed);
        if (!canEat && characterStats.Fullness == 0)
        {
            canEat = true;
            if (willEat)
            {
                willEat = false;
                EatFood();
            }
        }
    }

    protected virtual void AskForBoost()
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

        utilities[bestUtilityId].DoAction();
    }

    protected virtual void Attack()
    {
        OnAttack(characterStats.Attack);
    }

    void CheckMax()
    {
        if (characterStats.Life > maxHP)
            characterStats.Life = maxHP;

        if (characterStats.Mana > maxMP)
            characterStats.Mana = maxMP;

        Debug.Log("Current fullness = " + characterStats.Fullness + " / " + maxFullness);
        if (characterStats.Fullness > maxFullness)
            canEat = false;
    }

    public void TakeDamage(int strength)
    {
        int damage = Mathf.Max(0, strength - characterStats.Defense);
        characterStats.Life -= damage;
    }

    public bool IsDead()
    {
        return (characterStats.Life <= 0);
    }
}
