using UnityEngine;

public class Actor : MonoBehaviour
{
    public string Name;
    public Dialogue initialDialogue;
    public Dialogue alternateDialogue; // 👈 Shown after first talk
    private bool hasSpoken = false;

    public void SpeakTo()
    {
        if (!hasSpoken && initialDialogue != null)
        {
            DialogueManager.Instance.StartDialogue(Name, initialDialogue.RootNode);
            hasSpoken = true;
        }
        else if (alternateDialogue != null)
        {
            DialogueManager.Instance.StartDialogue(Name, alternateDialogue.RootNode);
        }
        else
        {
            Debug.Log($"{Name} has nothing new to say.");
        }
    }
}
