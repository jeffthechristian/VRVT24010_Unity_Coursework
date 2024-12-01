using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Bone : MonoBehaviour
{
    private bool isGrabbed = false;
    public GameObject highlight;
    private bool hasBeenGrabbed;

    void Start()
    {
        var grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (!hasBeenGrabbed)
        {
            Ghost.ghostAnger++;
        }

        hasBeenGrabbed = true;
        isGrabbed = true;
        highlight.SetActive(true);
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        isGrabbed = false;
        highlight.SetActive(false);
    }

    void Update()
    {
        if (isGrabbed)
        {
            Debug.Log("The object is currently grabbed.");
        }
    }
}
