using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRRigNonIK : MonoBehaviour
{
    public float turnSmoothness;

    public VRMap head;
    //public VRMap leftHand;
    //public VRMap rightHand;

    public Transform headConstraint;
    public Vector3 headBodyOffset;

    void Start()
    {
        headBodyOffset = transform.position - headConstraint.position;
    }

    void FixedUpdate()
    {
        transform.position = headConstraint.position + headBodyOffset;
        transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized, Time.deltaTime * turnSmoothness);

        head.Map();
        //leftHand.Map();
        //rightHand.Map();
    }
}
