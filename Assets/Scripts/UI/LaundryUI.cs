using UnityEngine;

public class LaundryUI : Singleton<LaundryUI>
{
    private void Start()
    {
        UpdateLaundryUI();    
    }

    public void UpdateLaundryUI()
    {
        for(int i = 0; i < 3; i++)
        {
            transform.GetChild(i).GetChild(0).gameObject.SetActive(PersistentData.collectedLaundry[i]);
        }
    }
}
