using UnityEngine;


[RequireComponent(typeof(Collectable))]
public class KeyCollectable : MonoBehaviour
{
    public int ID;

    private void Start()
    {
        GetComponent<Collectable>().onPick.AddListener(StoreKeyData);
    }

    private void StoreKeyData()
    {
        PersistentData.collectedKeys.Add(ID);
    }
}
