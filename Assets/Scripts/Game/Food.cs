using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]
    private GameObject cutFoodPrefab;

    [SerializeField]
    private int preparedFoodLayer;

    [SerializeField]
    private Stats statsToGive;

    public Stats StatBoost
    {
        get { return statsToGive; }
    }

    public void Cut()
    {
        InstantiateHalfFood("1");
        InstantiateHalfFood("2", true);

        Destroy(gameObject);
    }

    private void InstantiateHalfFood(string nameSuffix, bool rotate = false)
    {
        GameObject halfFood = Instantiate(cutFoodPrefab, transform.position, transform.rotation, null).gameObject;
        halfFood.name = "Half" + name + nameSuffix;
        halfFood.layer = preparedFoodLayer;
        if (rotate)
            halfFood.transform.Rotate(new Vector3(0f, 180f, 0f));
        halfFood.GetComponentInChildren<Food>().statsToGive = statsToGive;
    }
}
