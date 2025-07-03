using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DialogueList : MonoBehaviour
{
    public static DialogueList Instance { get; private set; }
    
    public List<DialogueData> dialogueDataList;
    public Dictionary<string, DialogueData> dialogueDictionary;


    private void Awake() {
        Instance = this;

        dialogueDictionary = new Dictionary<string, DialogueData>();

        foreach(DialogueData data in dialogueDataList) {
            dialogueDictionary[data.dialogue.name] = data;
        }
    }
    public DialogueData GetDialogueByTag(string tag) {
        if(dialogueDictionary.TryGetValue(tag, out DialogueData data)) {
            return data;
        }
        return null;
    }

}
