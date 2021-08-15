using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CookingTool : MonoBehaviour
{
    [SerializeField]
    private bool canCut = false;

    [SerializeField]
    private LayerMask foodmask;

    /// <summary>
	/// Update cutting state
	/// </summary>
	/// <param name="newCanCut">True to make the tool able to cut and false to remove its ability</param>
    public void CuttingState(bool newCanCut)
    {
        canCut = newCanCut;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canCut)
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
