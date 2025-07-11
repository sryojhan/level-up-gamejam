using UnityEngine;

public class LaundryCollectable : MonoBehaviour
{
    public int laundryNumber;

    private void Start()
    {
        GetComponent<Collectable>().onPick.AddListener(CollectLaundry);
    }

    private void CollectLaundry()
    {
        PersistentData.collectedLaundry[laundryNumber] = true;
        LaundryUI.instance.UpdateLaundryUI();
    }

}
