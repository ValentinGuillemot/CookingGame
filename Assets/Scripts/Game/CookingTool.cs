using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CookingTool : MonoBehaviour
{
    [SerializeField]
    private bool bCanCut = false;

    [SerializeField]
    private LayerMask foodmask;

    /* Start is called before the first frame update
    void Start()
    {
    }

    /* Update is called once per frame
    void Update()
    {
        
    }*/

    public void DebugCall()
    {
        Debug.Log("Debug has been called");
    }

    public void CuttingState(bool canCut)
    {
        bCanCut = canCut;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!bCanCut)
            return;

        if (foodmask == (foodmask | 1 << other.gameObject.layer))
        {
            GameObject foodParent = other.transform.parent.gameObject;
            Food foodToCut = foodParent.GetComponent<Food>();
            if (foodToCut)
                foodToCut.Cut();
        }
    }
}
