using UnityEngine;

public class Cross : MonoBehaviour
{
    public GameObject skullObject;
    public GameObject backboneObject;
    public GameObject bigBoneObject;
    public GameObject smallBone1Object;
    public GameObject smallBone2Object;
    public GameObject mediumBoneObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bones")) 
        {
            string itemName = other.gameObject.name;

            switch (itemName)
            {
                case "Skull":
                    EnableObject(skullObject);
                    break;
                case "Backbone":
                    EnableObject(backboneObject);
                    break;
                case "BigBone":
                    EnableObject(bigBoneObject);
                    break;
                case "SmallBone1":
                    EnableObject(smallBone1Object);
                    break;
                case "SmallBone2":
                    EnableObject(smallBone2Object);
                    break;
                case "MediumBone":
                    EnableObject(mediumBoneObject);
                    break;
                default:
                    Debug.Log("Item not recognized: " + itemName);
                    break;
            }

            Destroy(other.gameObject);
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
}
