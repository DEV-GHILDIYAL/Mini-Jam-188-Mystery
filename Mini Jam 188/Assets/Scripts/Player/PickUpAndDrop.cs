using UnityEngine;

public class PickUpAndDrop : MonoBehaviour
{
    public Transform DropPoint;  // Position where the object will be dropped
    public bool isPicked;        // Indicates whether an object is currently picked
    public GameObject objPicked; // The currently picked object

    private void Start()
    {
        isPicked = false;
    }

    private void Update()
    {
        // Only respond to input if an object is picked
        if (isPicked && objPicked != null)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                Debug.Log("Attempting to drop object...");
                RequestDrop();
            }
        }
    }

    public void RequestDrop()
    {
        if (objPicked != null)
        {
            Debug.Log("Dropping object: " + objPicked.name);
            DropObject(objPicked, DropPoint.position);
        }
    }

    private void DropObject(GameObject obj, Vector3 dropPosition)
    {
        // Set the position where the object will be dropped
        obj.transform.position = dropPosition;
        obj.SetActive(true); // Ensure the object is active
        Debug.Log("Object dropped at: " + dropPosition);

        // Reset the picked object state
        objPicked = null;
        isPicked = false;
    }

    public void Pick(GameObject obj)
    {
        if (!isPicked && objPicked == null)
        {
            Debug.Log("Picking up object: " + obj.name);
            objPicked = obj;
            isPicked = true;
            obj.SetActive(false); // Deactivate the object when picked
        }
    }
}
