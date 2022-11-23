using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotiveController : MonoBehaviour
{
    [SerializeField] private XRController leftTeleportRay;
    [SerializeField] private InputHelpers.Button teleportActivationButton;

    private bool CheckIfActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool isActivated, 0.1f);
        return isActivated;
    }
}
