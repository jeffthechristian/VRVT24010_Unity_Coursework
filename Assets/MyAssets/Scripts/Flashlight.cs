using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private GameObject flashlight;
    public static bool isFlashlight = false;
    public InputActionReference toggleFlashlightAction;

    private void OnEnable()
    {
        toggleFlashlightAction.action.performed += ToggleTabletActionPerformed;
        toggleFlashlightAction.action.Enable();
    }

    private void OnDisable()
    {
        toggleFlashlightAction.action.performed -= ToggleTabletActionPerformed;
        toggleFlashlightAction.action.Disable();
    }

    private void ToggleTabletActionPerformed(InputAction.CallbackContext context)
    {
        isFlashlight = !isFlashlight;
        flashlight.SetActive(isFlashlight);
    }
}
