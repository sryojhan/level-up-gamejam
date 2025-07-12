using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(UID))]
public class DialogueTrigger : MonoBehaviour
{
    public DialogueContent[] dialogues;

    private string entityUID;

    private int dialogueValue;

    Interactable interactable;

    private void Awake()
    {
        entityUID = GetComponent<UID>().uid;
        dialogueValue = PersistentData.Get(entityUID);

        interactable = GetComponent<Interactable>();

        interactable.onInteractionBegin += BeginDialogue;
    }


    public void BeginDialogue()
    {
        DialogueManager.instance.BeginDialogue(dialogues[dialogueValue], OnDialogueEnd);

        if(dialogueValue < dialogues.Length - 1)
        {
            dialogueValue++;
            PersistentData.Set(entityUID, dialogueValue);
        }
    }

    private void OnDialogueEnd()
    {
        interactable.EndInteraction();
    }
}
