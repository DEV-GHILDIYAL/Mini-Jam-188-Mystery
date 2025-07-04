using UnityEngine;

public class Interact : MonoBehaviour
{
    public Transform interactSource;
    public float interactDistance = 3f;
    public LayerMask interactLayer;

    public bool isInteracting;
    RaycastHit hit;

    private Interactable currentInteractable;

    private void Update()
    {
        CheckForInteraction();
        HandleInteractionInput();
    }

    private void CheckForInteraction()
    {
        Ray ray = new Ray(interactSource.position, interactSource.forward);
        Debug.DrawRay(interactSource.position, interactSource.forward * interactDistance, Color.green);

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
        if (!isInteracting || currentInteractable == null || hit.collider == null)
        {
            Debug.Log("Interaction failed.");
            return;
        }

        GameObject hitObj = hit.collider.gameObject;
        GameObject player = gameObject;

        switch (currentInteractable.interactableType)
        {
            case Interactable.InteractableType.Pickable:
                TryPick(player, hitObj);
                break;

            case Interactable.InteractableType.Normal:
                currentInteractable.Interact();
                break;

            case Interactable.InteractableType.NPC:
                TryTalkToNPC(currentInteractable);
                break;

            default:
                Debug.LogWarning("Unknown interactable type.");
                break;
        }
    }

    private void TryPick(GameObject player, GameObject hitObj)
    {
        PickUpAndDrop pickScript = player.GetComponentInChildren<PickUpAndDrop>();
        if (pickScript != null && !pickScript.isPicked)
        {
            pickScript.Pick(hitObj);
        }
        else
        {
            Debug.Log("Already holding something or pickup script not found.");
        }
    }

    private void TryTalkToNPC(Interactable npc)
    {
        Actor actor = npc.GetComponent<Actor>();

        if (actor != null)
        {
            actor.SpeakTo();

            if (npc.lookTarget != null)
            {
                LookTarget.instance.LookAt(npc.lookTarget);
            }
            else
            {
                Debug.LogWarning("Look target not set on NPC.");
            }
        }
        else
        {
            Debug.LogWarning("Actor script missing on NPC.");
        }
    }

}
