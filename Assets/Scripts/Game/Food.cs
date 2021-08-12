using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]
    private Mesh halfMesh;

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
        GameObject halfFood = Instantiate(this).gameObject;
        halfFood.name = "HalfApple" + nameSuffix;
        MeshFilter newMesh = halfFood.GetComponentInChildren<MeshFilter>();
        if (newMesh)
        {
            newMesh.mesh = halfMesh;
            newMesh.GetComponent<MeshCollider>().sharedMesh = halfMesh;
            if (rotate)
                newMesh.transform.Rotate(new Vector3(0f, 180f, 0f));
        }
        halfFood.layer = preparedFoodLayer;
        foreach (Transform childTransform in halfFood.GetComponentInChildren<Transform>())
        {
            childTransform.gameObject.layer = preparedFoodLayer;
        }
    }
}
