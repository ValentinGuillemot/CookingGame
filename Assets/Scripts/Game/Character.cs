using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    Stats characterStats;

    private int maxHP;
    private int maxMP;
    private float maxFullness;
    private bool canEat = true;
    private float digestSpeed = 2f;

    [SerializeField]
    XRSocketCustom plate;

    // Start is called before the first frame update
    void Start()
    {
        maxHP = characterStats.Life;
        maxMP = characterStats.Mana;
        maxFullness = characterStats.Fullness;

        // Runtime stats are created as a copy of the initial default stats
        characterStats = Instantiate<Stats>(characterStats);
        characterStats.Fullness = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Digestion (if fullness reaches 0, character can eat again)
        characterStats.Fullness = Mathf.Max(0, characterStats.Fullness - Time.deltaTime * digestSpeed);
        if (!canEat && characterStats.Fullness == 0)
            canEat = true;

        if (canEat && plate.HasSelectedItem())
        {
            Food toEat = plate.GetFoodFromPlate();
            if (toEat)
            {
                characterStats += toEat.StatBoost;
                CheckMax();
                plate.RemoveFood();
            }
        }
    }

    void CheckMax()
    {
        if (characterStats.Life > maxHP)
            characterStats.Life = maxHP;

        if (characterStats.Mana > maxMP)
            characterStats.Mana = maxMP;

        if (characterStats.Fullness > maxFullness)
            canEat = false;
    }

}
