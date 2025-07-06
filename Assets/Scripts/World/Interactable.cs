using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public enum Type
    {
        PickUp, Read, Inspect, Talk
    }

    public Type type;

    private string GetTypeMessage()
    {
        return type switch
        {
            Type.PickUp => "Press E to pick it up",
            Type.Read => "Press E to read",
            Type.Inspect => "Press E to inspect",
            Type.Talk => "Press E to talk",
            _ => "Press E to interact",
        };
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != PlayerController.instance.gameObject) return;

        InteractableUI.instance.Reveal(GetTypeMessage());
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject != PlayerController.instance.gameObject) return;

        InteractableUI.instance.Hide();
    }

    public UnityAction onInteractionBegin;
    public UnityAction onInteractionEnd;
}
