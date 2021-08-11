using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSocketCustom : XRSocketInteractor
{
    public override bool CanSelect(XRBaseInteractable interactable)
    {
        return (interactionLayerMask == (interactionLayerMask | 1 << interactable.gameObject.layer));
    }
}
