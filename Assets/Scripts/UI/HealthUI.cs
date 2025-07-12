using UnityEngine;
using UnityEngine.UI;


[DefaultExecutionOrder(100)]
public class HealthUI : MonoBehaviour
{
    public Color heartFilled = Color.red;
    public Color heartEmpty = Color.white;

    int previousHealth = -1;
    int previousMaxHealth = -1;

    public CoroutineAnimation revealNewHeart;

    public float heartAnimationVerticalOffset = 10;
    public CoroutineAnimation healHeart;


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
            bool updateColor = true;

            if(previousHealth > 0)
            {
                if(idx > newHealth && idx <= previousHealth)
                {
                    tr.GetComponentInChildren<ParticleSystem>().Play();
                }

                else if (idx > previousHealth && idx <= newHealth)
                {

                    RectTransform rt = tr.GetComponent<RectTransform>();

                    Vector2 originalPosition = rt.anchoredPosition;

                    void OnUpdate(float i)
                    {
                        if (i > 0.5f)
                        {
                            tr.GetComponentInChildren<Image>().color = heartFilled;
                        }


                        float offsetY = heartAnimationVerticalOffset * 4 * i * (1 - i);
                        rt.anchoredPosition = originalPosition + new Vector2(0, offsetY);
                    }

                    healHeart.Play(this, onUpdate: OnUpdate);

                    updateColor = false;
                }
            }

            if(updateColor)
                tr.GetComponentInChildren<Image>().color = idx > newHealth ? heartEmpty : heartFilled;

            idx++;
        }

        previousHealth = newHealth;
    }

    private void UpdateMaxHealth(int maxHealth)
    {
        int idx = 1;
        foreach (Transform tr in transform)
        {

            if(previousMaxHealth > 0 && idx == maxHealth)
            {
                void SetScale(float i)
                {
                    tr.localScale = Vector3.one * i;
                }

                revealNewHeart.Play(this, onUpdate: SetScale);
            }

            tr.GetComponentInChildren<Image>().enabled = idx++ <= maxHealth;
        }

        if (previousMaxHealth == previousHealth)
            previousHealth = maxHealth;

        previousMaxHealth = maxHealth;
    }


}
