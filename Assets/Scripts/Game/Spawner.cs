using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Food foodToCheck;

    [SerializeField]
    private GameObject prefabToSpawn;

    [SerializeField]
    private float timeToSpawn = 5f;
    private float currentTime = 0f;
    private bool isSpawning = false;

    void Start()
    {
        foodToCheck.onDestroy += SetSpawn;
        currentTime = timeToSpawn;
    }

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

    /// <summary>
	/// Allow the component to spawn game objects
	/// </summary>
    void SetSpawn()
    {
        isSpawning = true;
    }
}
