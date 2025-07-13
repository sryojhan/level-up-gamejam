using System;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(UID))]
public class Chest : MonoBehaviour
{
    string uid;
    private enum ChestState
    {
        Closed, Opened
    }

    public Sprite openedChest;
    public Sprite chestContentSprite;

    public enum ChestContent
    {
        Heal, Heart, MoveSpeed, Damage, Sock
    }

    public ChestContent chestContent;


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
            Destroy(interactable);
            chestSpriteRenderer.sprite = openedChest;
            return;
        }

        interactable.onInteractionBegin += BeginDialogue;
    }

    private void OpenChest()
    {
        PersistentData.Set(uid, (int)ChestState.Opened);

        interactable.EndInteraction();
        interactable.ClearOutline();


        switch (chestContent)
        {
            case ChestContent.Heal:
                HealOneHeart();
                break;
            case ChestContent.Heart:
                GainExtraHeart();
                break;
            case ChestContent.MoveSpeed:
                GainMoveSpeed();
                break;
            case ChestContent.Damage:
                GainDamageIncrease();
                break;
            case ChestContent.Sock:
                GainExtraSock();
                break;
            default:
                break;
        }


        Destroy(interactable);
        chestSpriteRenderer.sprite = openedChest;

        SoundManager.instance.PlayUIBoing();
    }

    private void BeginDialogue()
    {
        SoundManager.instance.PlayCollectItem();

        DialogueManager.instance.BeginDialogue(unlockContent, AfterDialogue, chestContentSprite);
    }

    private void AfterDialogue()
    {
        OpenChest();
    }



    public void GainExtraHeart()
    {
        PlayerController.instance.PlayerHealth.AddMaxHealth();
    }

    public void HealOneHeart()
    {
        PlayerController.instance.PlayerHealth.RestoreHealth();
    }

    public void GainMoveSpeed()
    {
        PlayerController.instance.Movement.ApplyIncrease();
    }

    public void GainDamageIncrease()
    {
        PersistentData.damageIncrease = true;
    }

    public void GainExtraSock()
    {
        PlayerController.instance.SockLauncher.AddOneMaxSock();
    }
}
