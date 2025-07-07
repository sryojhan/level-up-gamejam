using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public enum Type
    {
        PickUpKey, PickUpCoin, Read, Inspect, Talk
    }

    public Type type;

    private SpriteRenderer spriteRenderer;
    private MaterialPropertyBlock mpb;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        mpb = new MaterialPropertyBlock();
    }


    private string GetTypeMessage()
    {
        return type switch
        {
            Type.PickUpKey => Localization.GetText("pick_key"),
            Type.PickUpCoin => Localization.GetText("pick_coin"),
            Type.Read => Localization.GetText("read"),
            Type.Inspect => Localization.GetText("inspect"),
            Type.Talk => Localization.GetText("talk"),
            _ => Localization.GetText("interact")
        };
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != PlayerController.instance.gameObject) return;

        InteractableUI.instance.Reveal(GetTypeMessage());


        spriteRenderer.GetPropertyBlock(mpb);
        mpb.SetFloat("_useOutline", 1);
        spriteRenderer.SetPropertyBlock(mpb);
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject != PlayerController.instance.gameObject) return;

        InteractableUI.instance.Hide();


        spriteRenderer.GetPropertyBlock(mpb);
        mpb.SetFloat("_useOutline", 0);
        spriteRenderer.SetPropertyBlock(mpb);
    }

    public UnityAction onInteractionBegin;
    public UnityAction onInteractionEnd;
}
