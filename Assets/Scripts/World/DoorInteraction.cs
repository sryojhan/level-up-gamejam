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

    private void Start()
    {
        uid = GetComponent<UID>().uid;

        if(PersistentData.Get(uid) == (int) DoorState.Opened)
        {
            Destroy(gameObject);
            return;
        }

        GetComponent<Interactable>().onInteractionBegin.AddListener(OpenDoor);
    }

    private void OpenDoor()
    {
        if (!PersistentData.collectedKeys.Contains(id))
        {
            GetComponentInChildren<SimpleShake>().Shake();
            return;
        }

        PersistentData.Set(uid, (int)DoorState.Opened);
        Destroy(gameObject);
    }

}
