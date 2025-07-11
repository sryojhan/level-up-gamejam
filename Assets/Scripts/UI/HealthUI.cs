using UnityEngine;
using UnityEngine.UI;


[DefaultExecutionOrder(100)]
public class HealthUI : MonoBehaviour
{
    public Color heartFilled = Color.red;
    public Color heartEmpty = Color.white;

    int previousHealth = -1;

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
            if(previousHealth > 0 && newHealth < previousHealth && idx == previousHealth)
            {
                tr.GetComponentInChildren<ParticleSystem>().Play();
            }

            tr.GetComponentInChildren<Image>().color = idx++ > newHealth ? heartEmpty : heartFilled;
        }

        previousHealth = newHealth;
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
