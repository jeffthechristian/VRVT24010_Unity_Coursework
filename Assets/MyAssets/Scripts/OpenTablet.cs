using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class OpenTablet : MonoBehaviour
{
    [SerializeField] private GameObject tablet;

    public InputActionReference toggleTabletAction; 

    private void OnEnable()
    {
        toggleTabletAction.action.performed += ToggleTabletActionPerformed;
        toggleTabletAction.action.Enable();
    }

    private void OnDisable()
    {
        toggleTabletAction.action.performed -= ToggleTabletActionPerformed;
        toggleTabletAction.action.Disable();
    }

    private void ToggleTabletActionPerformed(InputAction.CallbackContext context)
    {
        tablet.SetActive(!tablet.activeSelf);
    }
}
