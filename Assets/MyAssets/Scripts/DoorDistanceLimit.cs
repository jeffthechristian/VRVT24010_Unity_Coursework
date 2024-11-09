using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DoorDistanceLimit : MonoBehaviour
{
    public XRGrabInteractable grabInteractable;
    public Transform leftControllerTransform;
    public Transform rightControllerTransform;
    public float maxGrabDistance = 1.0f;

    private void Update()
    {
        if (grabInteractable.isSelected)
        {
            float leftDistance = Vector3.Distance(leftControllerTransform.position, transform.position);
            float rightDistance = Vector3.Distance(rightControllerTransform.position, transform.position);

            if (leftDistance > maxGrabDistance || rightDistance > maxGrabDistance)
            {
                grabInteractable.interactionManager.CancelInteractableSelection(grabInteractable as IXRSelectInteractable);
                Debug.Log("Cancelled interaction due to distance of the object");

            }
        }
    }
}
