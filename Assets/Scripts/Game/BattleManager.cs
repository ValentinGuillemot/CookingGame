using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    private List<Character> party;

    [SerializeField]
    private float timeBetweenEnemyAttacks = 5f;
    private float currentTime = 0f;

    [SerializeField]
    private int enemyStrength;

    //Scores
    public float timeScore = 0f;
    public int damageScore = 0;

    // UI
    [SerializeField]
    private TextMeshPro timeDisplay;
    [SerializeField]
    private TextMeshPro scoreDisplay;

    void Start()
    {
        currentTime = timeBetweenEnemyAttacks;

        for (int i = 0; i < party.Count; i++)
        {
            party[i].OnAttack += UpdateDamageScore;
        }

        timeDisplay.text = "00:00";
        scoreDisplay.text = "0";
    }

    void Update()
    {
        if (party.Count <= 0)
            return;

        currentTime -= Time.deltaTime;
        if (currentTime <= 0f)
        {
            AttackCharacter();
            currentTime = timeBetweenEnemyAttacks;
        }

        // Update time UI
        timeScore += Time.deltaTime;
        int nbOfSec = (int)timeScore;
        int nbOfDec = (int)((timeScore - nbOfSec) * 100f);
        string decimalPart = (nbOfDec < 10) ? "0" + nbOfDec.ToString() : nbOfDec.ToString();
        timeDisplay.text = nbOfSec.ToString() + ":" + decimalPart;
    }

    /// <summary>
    /// Deal damage to one random character in the party
    /// </summary>
    void AttackCharacter()
    {
        int target = Random.Range(0, party.Count);
        party[target].TakeDamage(enemyStrength);

        if (party[target].IsDead())
        {
            GameObject defeatedCharacter = party[target].gameObject;
            party.RemoveAt(target);
            Destroy(defeatedCharacter);
        }
    }

    /// <summary>
    /// Called each time a character attacks
    /// </summary>
    /// <param name="attackStrength">Damage dealt by the character attack</param>
    void UpdateDamageScore(int attackStrength)
    {
        damageScore += attackStrength;
        scoreDisplay.text = damageScore.ToString();
    }
}
