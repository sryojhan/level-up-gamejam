using UnityEngine;


[RequireComponent(typeof(UID))]
[RequireComponent(typeof(Interactable))]
public class DoorInteraction : MonoBehaviour
{
    private enum DoorState
    {
        Closed, Opened
    }

    public int id;

    private string uid;

    private Interactable interactable;


    private void Start()
    {
        uid = GetComponent<UID>().uid;

        if(PersistentData.Get(uid) == (int) DoorState.Opened)
        {
            Destroy(gameObject);
            return;
        }
        interactable = GetComponent<Interactable>();
        interactable.onInteractionBegin += OpenDoor;
    }

    private void OpenDoor()
    {
        if (!PersistentData.collectedKeys.Contains(id))
        {
            GetComponentInChildren<SimpleShake>().Shake();
            interactable.EndInteraction();

            SoundManager.instance.PlayLockedDoor();
            return;
        }

        PersistentData.Set(uid, (int)DoorState.Opened);
        Destroy(gameObject);

        interactable.EndInteraction();

        SoundManager.instance.PlayOpenDoor();

    }

}
