//Actor
using UnityEngine;

public class Actor : MonoBehaviour {
    public static Actor Instance { get; private set; }

    public void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }
    //public string Name;
    //public Dialogue Dialogue;

    //private void Update() {
    //    if (Input.GetKeyDown(KeyCode.Space)) {
    //        SpeakTo();
    //    }
    //}

    // Trigger dialogue for this actor
    public void SpeakTo(string Name, Dialogue Dialogue) {
        DialogueManager.Instance.StartDialogue(Name, Dialogue.RootNode);
    }
}
