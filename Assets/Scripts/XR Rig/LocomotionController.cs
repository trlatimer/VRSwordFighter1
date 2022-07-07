using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionController : MonoBehaviour
{
    public ActionBasedController leftTeleportRay;
    public ActionBasedController rightTeleportRay;
    public InputHelpers.Button teleportationActivationButton;
    public float activationThreshold = 0.1f;

    public XRRayInteractor leftInteractorRay;
    public XRRayInteractor rightInteractorRay;

    public bool EnableLeftTeleport { get; set; } = true;
    public bool EnableRightTeleport { get; set; } = true;

    void Update()
    {
        if (leftTeleportRay)
        {
            bool isLeftInteractorRayHovering = leftInteractorRay.TryGetHitInfo(out Vector3 pos, out Vector3 norm, out int index, out bool validTarget);
            leftTeleportRay.gameObject.SetActive(EnableLeftTeleport && CheckIfActivated(leftTeleportRay) && !isLeftInteractorRayHovering);
        }
        if (rightTeleportRay)
        {
            bool isRightInteractorRayHovering = rightInteractorRay.TryGetHitInfo(out Vector3 pos, out Vector3 norm, out int index, out bool validTarget);
            rightTeleportRay.gameObject.SetActive(EnableRightTeleport && CheckIfActivated(rightTeleportRay) && !isRightInteractorRayHovering);
        }
    }

    public bool CheckIfActivated(ActionBasedController controller)
    {
        return controller.activateAction.action.ReadValue<float>() > 0;
    }
}
