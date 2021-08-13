using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    List<Character> party;

    [SerializeField]
    float timeBetweenEnemyAttacks = 5f;
    float currentTime = 0f;

    [SerializeField]
    int enemyStrength;

    //Scores
    public float timeScore = 0f;
    public int damageScore = 0;

    // UI
    [SerializeField]
    TextMeshPro timeDisplay;
    [SerializeField]
    TextMeshPro scoreDisplay;

    // Start is called before the first frame update
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

    // Update is called once per frame
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

        timeScore += Time.deltaTime;
        int nbOfSec = (int)timeScore;
        int nbOfDec = (int)((timeScore - nbOfSec) * 100f);
        string decimalPart = (nbOfDec < 10) ? "0" + nbOfDec.ToString() : nbOfDec.ToString();
        timeDisplay.text = nbOfSec.ToString() + ":" + decimalPart;
    }

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

    // Called each time a character attacks;
    void UpdateDamageScore(int attackStrength)
    {
        damageScore += attackStrength;
        scoreDisplay.text = damageScore.ToString();
    }
}
