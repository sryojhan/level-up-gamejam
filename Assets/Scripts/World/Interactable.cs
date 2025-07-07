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

    public CoroutineAnimation thicknessAnimation;

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

        void onBegin()
        {
            spriteRenderer.GetPropertyBlock(mpb);
            mpb.SetFloat("_useOutline", 1);
            spriteRenderer.SetPropertyBlock(mpb);
        }

        void onUpdate(float i)
        {
            float targetThickness = 0.01f;

            spriteRenderer.GetPropertyBlock(mpb);
            mpb.SetFloat("_thickness", targetThickness * i);
            spriteRenderer.SetPropertyBlock(mpb);
        }

        thicknessAnimation.Play(this, onUpdate: onUpdate, onBegin: onBegin);

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject != PlayerController.instance.gameObject) return;

        if (InteractableUI.IsInitialised())
            InteractableUI.instance.Hide();



        void onUpdate(float i)
        {
            i = 1 - i;

            float targetThickness = 0.01f;

            spriteRenderer.GetPropertyBlock(mpb);
            mpb.SetFloat("_thickness", targetThickness * i);
            spriteRenderer.SetPropertyBlock(mpb);
        }

        void onEnd()
        {
            spriteRenderer.GetPropertyBlock(mpb);
            mpb.SetFloat("_useOutline", 0);
            spriteRenderer.SetPropertyBlock(mpb);
        }

        thicknessAnimation.Play(this, onUpdate: onUpdate, onEnd: onEnd);

    }

    public UnityAction onInteractionBegin;
    public UnityAction onInteractionEnd;
}
