using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool canPick;
    public GameObject interactUI; // <-- This will be shown when looked at

    private void Start()
    {
        if (interactUI != null)
        {
            interactUI.SetActive(false);
        }
    }

    public void ShowUI()
    {
        if (interactUI != null)
        {
            interactUI.SetActive(true);
        }
    }

    public void HideUI()
    {
        if (interactUI != null)
        {
            interactUI.SetActive(false);
        }
    }

    public void Interact()
    {
        // Optional: implement logic if you want this method used
    }
}
