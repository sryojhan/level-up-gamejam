using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(UID))]
public class Collectable : MonoBehaviour
{
    private string uid;

    public UnityEvent onPick;

    private void Start()
    {
        uid = GetComponentInChildren<UID>().uid;

        if(PersistentData.GetNPC(uid) != 0)
        {
            Destroy(gameObject);
            return;
        }
    }


    public void Collect()
    {
        onPick?.Invoke();

        PersistentData.SetNPC(uid, 1);
        Destroy(gameObject);
    }


}
