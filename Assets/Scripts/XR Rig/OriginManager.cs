using Unity.XR.CoreUtils;
using UnityEngine;

public class OriginManager : MonoBehaviour
{
    public float gravity = -9.81f;
    public LayerMask groundLayer;
    public float additionalHeight = 0.2f;

    

    private float fallingSpeed;
    private XROrigin origin;
    private CharacterController character;

    private void Start()
    {
        character = GetComponent<CharacterController>();
        origin = GetComponent<XROrigin>();
    }

    private void FixedUpdate()
    {
        CapsuleFollowHeadset();

        bool isGrounded = CheckIfGrounded();
        if (isGrounded)
            fallingSpeed = 0;
        else
            fallingSpeed += gravity * Time.fixedDeltaTime;

        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }

    void CapsuleFollowHeadset()
    {
        character.height = origin.CameraInOriginSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(origin.Camera.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z);
    }

    bool CheckIfGrounded()
    {
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit rayInfo, rayLength, groundLayer);
        return hasHit;
    }
}
