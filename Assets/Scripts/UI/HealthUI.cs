using UnityEngine;
using UnityEngine.UI;


[DefaultExecutionOrder(100)]
public class HealthUI : MonoBehaviour
{
    private void Awake()
    {
        PlayerController.instance.PlayerHealth.onHealthUpdate += UpdateHealth;
        PlayerController.instance.PlayerHealth.onMaxHealthUpdate += UpdateMaxHealth;
    }


    private void UpdateHealth(int newHealth)
    {
        int idx = 1;
        foreach (Transform tr in transform)
        {
            tr.GetComponentInChildren<Image>().color = idx++ > newHealth ? Color.white : Color.red;
        }
    }

    private void UpdateMaxHealth(int maxHealth)
    {
        int idx = 1;
        foreach (Transform tr in transform)
        {
            tr.GetComponentInChildren<Image>().enabled = idx++ <= maxHealth;
        }
    }


}
