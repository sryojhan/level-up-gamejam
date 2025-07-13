using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public enum Type
    {
        PickUpKey, PickUpCoin, Read, Inspect, Talk, OpenDoor, PickUpLaundry, OpenChest
    }

    public Type type;

    private SpriteRenderer spriteRenderer;
    private MaterialPropertyBlock mpb;

    private float materialThickness;


    public delegate void Callback();

    public Callback onInteractionBegin;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        mpb = new MaterialPropertyBlock();

        materialThickness = spriteRenderer.material.GetFloat("_thickness");
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
            Type.OpenDoor => Localization.GetText("open_door"),
            Type.PickUpLaundry => Localization.GetText("pick_laundry"),
            Type.OpenChest => Localization.GetText("open_chest"),
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

            spriteRenderer.GetPropertyBlock(mpb);
            mpb.SetFloat("_thickness", materialThickness * i);
            spriteRenderer.SetPropertyBlock(mpb);
        }

        thicknessAnimation.Play(this, onUpdate: onUpdate, onBegin: onBegin);


        PlayerController.instance.Interactions.ApproachInteractable(this);
    }


    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject != PlayerController.instance.gameObject) return;

        if (InteractableUI.IsInitialised())
            InteractableUI.instance.Hide();

        void onUpdate(float i)
        {
            i = 1 - i;

            float targetThickness = mpb.GetFloat("_thickness");

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

        PlayerController.instance.Interactions.LeaveInteractable();
    }


    private bool inInteraction = false;

    public void BeginInteraction()
    {
        if (inInteraction) return;

        inInteraction = true;

        PlayerController.instance.SetAllControls(false);
        onInteractionBegin?.Invoke();
    }

    public void EndInteraction()
    {
        inInteraction = false;
        PlayerController.instance.SetAllControls(true);
    }


    public void ClearOutline()
    {
        spriteRenderer.GetPropertyBlock(mpb);
        mpb.SetFloat("_useOutline", 0);
        spriteRenderer.SetPropertyBlock(mpb);
    }
}
