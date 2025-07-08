using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{

    private Interactable interactableNearMe = null;

    public bool CanInteract(bool interactButtonPressed)
    {
        return interactButtonPressed && interactableNearMe != null;
    }

    public void Interact()
    {
        interactableNearMe.BeginInteraction();
    }

    public void ApproachInteractable(Interactable interactableElement)
    {
        interactableNearMe = interactableElement; 
    }

    public void LeaveInteractable()
    {
        interactableNearMe = null;
    }
}
