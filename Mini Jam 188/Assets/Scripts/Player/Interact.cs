using UnityEngine;

public class Interact : MonoBehaviour
{
    public Transform interactSource;
    public float interactDistance = 3f;
    public LayerMask interactLayer;

    public bool isInteracting;
    RaycastHit hit;
    public Transform npcTarget;

    Interactable currentInteractable; // Keep track of last interactable seen

    private void Update()
    {
        CheckForInteraction();
        HandleInteractionInput();
    }

    private void CheckForInteraction()
    {
        Ray ray = new Ray(interactSource.position, interactSource.forward);
        Debug.DrawRay(interactSource.position, interactSource.forward, Color.green);

        if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
        {
            if (hit.collider.TryGetComponent(out Interactable interactable))
            {
                if (!isInteracting)
                {
                    interactable.ShowUI();
                    isInteracting = true;
                    currentInteractable = interactable;
                }
            }
        }
        else
        {
            if (isInteracting && currentInteractable != null)
            {
                currentInteractable.HideUI();
                currentInteractable = null;
                isInteracting = false;
            }
        }
    }

    private void HandleInteractionInput()
    {
        if (isInteracting && Input.GetKeyDown(KeyCode.E))
        {
            DoInteract();
        }
    }

    public void DoInteract()
    {
        if (isInteracting && currentInteractable != null)
        {
            GameObject hitObj = hit.collider.gameObject;
            GameObject player = gameObject;

            if (player == null)
            {
                Debug.LogWarning("Player not found.");
                return;
            }

            if (!currentInteractable.canPick)
            {
                HandleNonPickupInteraction(hitObj, player);
            }
            else if (!player.GetComponentInChildren<PickUpAndDrop>().isPicked)
            {
                player.GetComponentInChildren<PickUpAndDrop>().Pick(hit.collider.gameObject);
            }
        }
        else
        {
            Debug.Log("Interaction failed.");
        }
    }

    private void HandleNonPickupInteraction(GameObject hitObj, GameObject player)
    {
        string tag = hitObj.tag;
        DialogueData data = DialogueList.Instance.GetDialogueByTag(tag);
        if (data != null)
        {
            Actor.Instance.SpeakTo(data.characterName, data.dialogue);
            LookTarget.instance.LookAt(npcTarget);
        }
        else
        {
            Debug.Log("No data found for this tag: " + tag);
        }
    }
}
