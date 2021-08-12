using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        currentTime = timeBetweenEnemyAttacks;

        for (int i = 0; i < party.Count; i++)
        {
            party[i].OnAttack += UpdateDamageScore;
        }
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
    }
}
