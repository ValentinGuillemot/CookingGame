using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// Custom Socket used for the plates
public class XRSocketCustom : XRSocketInteractor
{
    public delegate void SelectFoodEvent();
    public SelectFoodEvent onSelectFood;

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        // Use the XRSocket layermask to check if interactable can be selected (not working with default XRSocketInteractor
        bool result = (interactionLayerMask == (interactionLayerMask | 1 << interactable.gameObject.layer));
        if (result && onSelectFood != null)
            onSelectFood();

        return result;
    }

    public bool HasSelectedItem()
    {
        return (selectTarget != null);
    }

    /// <summary>
	/// Return Food component of currently selected interactable
	/// </summary>
    public Food GetFoodFromPlate()
    {
        if (!selectTarget)
            return null;

        return selectTarget.GetComponent<Food>();
    }

    /// <summary>
	/// Destroy currently selected interactable
	/// </summary>
    public void RemoveFood()
    {
        if (selectTarget)
            Destroy(selectTarget.gameObject);
    }
}
