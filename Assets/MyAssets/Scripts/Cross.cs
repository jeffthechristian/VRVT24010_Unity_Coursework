using UnityEngine;

public class Cross : MonoBehaviour
{
    public GameObject skullObject;
    public GameObject backboneObject;
    public GameObject bigBoneObject;
    public GameObject smallBone1Object;
    public GameObject smallBone2Object;
    public GameObject mediumBoneObject;

    public GameObject cabinetDoor1;
    public GameObject cabinetDoor2;
    public GameObject candle;

    public GameObject fire;
    public GameObject victory;
    public AudioSource audiosource;
    public AudioClip sound2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bones"))
        {
            string itemName = other.gameObject.name;

            switch (itemName)
            {
                case "Skull(Clone)":
                    EnableObject(skullObject);
                    break;
                case "Backbone(Clone)":
                    EnableObject(backboneObject);
                    break;
                case "BigBone(Clone)":
                    EnableObject(bigBoneObject);
                    break;
                case "SmallBone1(Clone)":
                    EnableObject(smallBone1Object);
                    break;
                case "SmallBone2(Clone)":
                    EnableObject(smallBone2Object);
                    break;
                case "MediumBone(Clone)":
                    EnableObject(mediumBoneObject);
                    break;
                default:
                    Debug.Log("Item not recognized: " + itemName);
                    break;
            }

            Destroy(other.gameObject);

            if (AreAllBonesEnabled())
            {
                if (cabinetDoor1 != null) cabinetDoor1.SetActive(false);
                if (cabinetDoor2 != null) cabinetDoor2.SetActive(true);
                if (candle != null) candle.SetActive(true);

                audiosource.PlayOneShot(sound2);
            }
        }

        if (other.CompareTag("Fire"))
        {
            fire.SetActive(true);
            victory.SetActive(true);
        }
    }

    private void EnableObject(GameObject obj)
    {
        if (obj != null)
        {
            obj.SetActive(true);
        }
        else
        {
            Debug.LogError("Object is not assigned in the script.");
        }
    }

    private bool AreAllBonesEnabled()
    {
        return skullObject.activeInHierarchy &&
               backboneObject.activeInHierarchy &&
               bigBoneObject.activeInHierarchy &&
               smallBone1Object.activeInHierarchy &&
               smallBone2Object.activeInHierarchy &&
               mediumBoneObject.activeInHierarchy;
    }
}
