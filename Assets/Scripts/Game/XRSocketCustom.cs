using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// Custom Socket used for the plates
public class XRSocketCustom : XRSocketInteractor
{
    public override bool CanSelect(XRBaseInteractable interactable)
    {
        return (interactionLayerMask == (interactionLayerMask | 1 << interactable.gameObject.layer));
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
