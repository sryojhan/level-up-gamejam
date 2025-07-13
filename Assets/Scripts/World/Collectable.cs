using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(UID))]
public class Collectable : MonoBehaviour
{
    public Interactable.Callback onPick;

    private enum CollectableState
    {
        NotCollected, Collected
    }

    private string uid;

    private Interactable interactable;

    private void Start()
    {
        uid = GetComponentInChildren<UID>().uid;

        if(PersistentData.Get(uid) != (int)CollectableState.NotCollected)
        {
            Destroy(gameObject);
            return;
        }

        interactable = GetComponent<Interactable>();
        interactable.onInteractionBegin += Collect;
    }


    public void Collect()
    {
        onPick?.Invoke();

        PersistentData.Set(uid, (int)CollectableState.Collected);
        Destroy(gameObject);

        interactable.EndInteraction();


        SoundManager.instance.PlayCollectItem();
    }


}
