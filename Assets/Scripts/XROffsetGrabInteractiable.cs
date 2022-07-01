using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetGrabInteractiable : XRGrabInteractable
{
    private Vector3 initialAttachLocalPos;
    private Quaternion initialAttachLocalRot;

    void Start()
    {
        if (!attachTransform)
        {
            GameObject grab = new GameObject("Grab Pivot");
            grab.transform.SetParent(transform, false);
            attachTransform = grab.transform;
        }

        initialAttachLocalPos = attachTransform.localPosition;
        initialAttachLocalRot = attachTransform.localRotation;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject is XRDirectInteractor)
        {
            Debug.Log("Entered interactable as XRDirectInteractor");
            XRDirectInteractor interactor = (XRDirectInteractor)args.interactorObject;
            attachTransform.position = interactor.transform.position;
            attachTransform.rotation = interactor.transform.rotation;
        }
        else
        {
            Debug.Log("Entered interactable, not XRDirectInteractor");
            attachTransform.localPosition = initialAttachLocalPos;
            attachTransform.localRotation = initialAttachLocalRot;
        }

        base.OnSelectEntered(args);
    }
}
