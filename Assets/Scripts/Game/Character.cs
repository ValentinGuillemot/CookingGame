using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    Stats characterStats;

    private int maxHP;
    private int maxMP;

    // Start is called before the first frame update
    void Start()
    {
        maxHP = characterStats.Life;
        maxMP = characterStats.Mana;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
