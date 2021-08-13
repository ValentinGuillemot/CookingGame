using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    Food foodToCheck;

    [SerializeField]
    GameObject prefabToSpawn;

    [SerializeField]
    float timeToSpawn = 5f;
    float currentTime = 0f;
    private bool isSpawning;

    // Start is called before the first frame update
    void Start()
    {
        foodToCheck.onDestroy += SetSpawn;
        currentTime = timeToSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawning)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0f)
            {
                currentTime = timeToSpawn;
                foodToCheck = Instantiate(prefabToSpawn, transform).GetComponent<Food>();
                foodToCheck.onDestroy += SetSpawn;
                isSpawning = false;
            }
        }
    }

    void SetSpawn()
    {
        isSpawning = true;
    }
}
