using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Unity.XR.CoreUtils
{
    public class Crouch : MonoBehaviour
    {
        public XROrigin xrOrigin;
        public InputActionReference toggleCrouchlightAction;

        private void OnEnable()
        {
            toggleCrouchlightAction.action.performed += ToggleTabletActionPerformed;
            toggleCrouchlightAction.action.Enable();
        }

        private void OnDisable()
        {
            toggleCrouchlightAction.action.performed -= ToggleTabletActionPerformed;
            toggleCrouchlightAction.action.Disable();
        }

        private void ToggleTabletActionPerformed(InputAction.CallbackContext context)
        {
            xrOrigin.ToggleCrouch();
        }
    }
}