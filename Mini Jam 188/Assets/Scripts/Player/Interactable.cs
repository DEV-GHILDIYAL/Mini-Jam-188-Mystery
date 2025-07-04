using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool canPick;
    public GameObject interactUI;

    public Transform lookTarget; // 👈 NEW: where the player/NPC should look during dialogue

    public enum InteractableType { Pickable, Normal, NPC }

    public InteractableType interactableType;

    private void Start()
    {
        if (interactUI != null)
            interactUI.SetActive(false);
    }

    public void ShowUI()
    {
        if (interactUI != null)
            interactUI.SetActive(true);
    }

    public void HideUI()
    {
        if (interactUI != null)
            interactUI.SetActive(false);
    }

    public void Interact()
    {
        
    }
}
