using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    protected Stats characterStats;

    private int maxHP;
    private int maxMP;
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
