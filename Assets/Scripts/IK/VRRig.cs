using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

[System.Serializable]
public class VRMap
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void Map()
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}

public class VRRig : MonoBehaviour
{
    public float turnSmoothness;

    public VRMap head;
    public VRMap leftHand;
    public VRMap rightHand;

    public Transform headConstraint;
    public Vector3 headBodyOffset;

    public PhotonView photonView;

    void Start()
    {
        headBodyOffset = transform.position - headConstraint.position;
        photonView = GetComponentInParent<PhotonView>();

        if (photonView == null || photonView != null && photonView.IsMine)
        {
            if (head.vrTarget == null || leftHand.vrTarget == null || rightHand.vrTarget == null)
            {
                XROrigin origin = FindObjectOfType<XROrigin>();
                head.vrTarget = origin.transform.Find("Camera Offset/Main Camera");
                leftHand.vrTarget = origin.transform.Find("Camera Offset/LeftHand Controller");
                rightHand.vrTarget = origin.transform.Find("Camera Offset/RightHand Controller");
            }
        }
    }

    void FixedUpdate()
    {
        if (photonView == null || photonView != null && photonView.IsMine) // If null, we are in a lobby screen with no networked player
        {
            transform.position = headConstraint.position + headBodyOffset;
            transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized, Time.deltaTime * turnSmoothness);

            head.Map();
            leftHand.Map();
            rightHand.Map();
        }     
    }
}
