using UnityEngine;


[RequireComponent(typeof(UID))]
public class DialogueWorldData : Interactable
{
    public DialogueContent[] dialogues;

    private string entityUID;

    private void Awake()
    {
        entityUID = GetComponent<UID>().uid;
    }

}
