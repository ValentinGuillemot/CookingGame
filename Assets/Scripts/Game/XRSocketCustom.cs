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
        bool result = (interactionLayerMask == (interactionLayerMask | 1 << interactable.gameObject.layer));
        if (result && onSelectFood != null)
            onSelectFood();

        return result;
    }

    public bool HasSelectedItem()
    {
        return (selectTarget != null);
    }

    public Food GetFoodFromPlate()
    {
        if (!selectTarget)
            return null;

        return selectTarget.GetComponent<Food>();
    }

    public void RemoveFood()
    {
        if (selectTarget)
            Destroy(selectTarget.gameObject);
    }
}
