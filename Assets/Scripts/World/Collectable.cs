using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(UID))]
public class Collectable : MonoBehaviour
{

    private enum CollectableState
    {
        NotCollected, Collected
    }

    private string uid;

    public UnityEvent onPick;

    private void Start()
    {
        uid = GetComponentInChildren<UID>().uid;

        if(PersistentData.Get(uid) != (int)CollectableState.NotCollected)
        {
            Destroy(gameObject);
            return;
        }
    }


    public void Collect()
    {
        onPick?.Invoke();

        PersistentData.Set(uid, (int)CollectableState.Collected);
        Destroy(gameObject);
    }


}
