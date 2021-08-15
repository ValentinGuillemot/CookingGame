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

    public delegate void DestroyEvent();
    public DestroyEvent onDestroy;

    public Stats StatBoost
    {
        get { return statsToGive; }
    }

    /// <summary>
	/// Cut the food in two new game objects
	/// </summary>
    public void Cut()
    {
        InstantiateHalfFood("1");
        InstantiateHalfFood("2", true);

        if (onDestroy != null)
            onDestroy();

        Destroy(gameObject);
    }

    /// <summary>
	/// Instantiate a half of the cut object
	/// </summary>
	/// <param name="nameSuffix">Suffix at the end of new gameObject name</param>
	/// <param name="rotate">Whether the gameObject should be rotated (180 degrees along y axis) or not</param>
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
