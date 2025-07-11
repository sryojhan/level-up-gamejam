using System;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(UID))]
[RequireComponent(typeof(Interactable))]
public class Chest : MonoBehaviour
{
    string uid;
    private enum ChestState
    {
        Closed, Opened
    }

    public Sprite openedChest;
    public Sprite chestContent;

    Interactable interactable;

    SpriteRenderer chestSpriteRenderer;

    public DialogueContent unlockContent;

    private void Start()
    {
        uid = GetComponent<UID>().uid;
        interactable = GetComponent<Interactable>();
        chestSpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if(PersistentData.Get(uid) != (int)ChestState.Closed)
        {
            OpenChest();
            return;
        }

        interactable.onInteractionBegin.AddListener(BeginDialogue);
    }

    private void OpenChest()
    {
        Destroy(interactable);
        chestSpriteRenderer.sprite = openedChest;
    }

    private void BeginDialogue()
    {
        DialogueManager.instance.BeginDialogue(unlockContent, AfterDialogue);
    }

    private void AfterDialogue()
    {
        unlockAfterDialogue?.Invoke();
    }

    public UnityEvent unlockAfterDialogue;
}
