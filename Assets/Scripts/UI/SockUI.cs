using UnityEngine;
using UnityEngine.UI;

public class SockUI : MonoBehaviour
{
    Slider[] sliders;

    Slider currentSlider;

    private void Awake()
    {
        PlayerController.instance.SockLauncher.onSockCountUpdate += OnSockCountUpdate;
        PlayerController.instance.SockLauncher.onMaxSockCountUpgrade += OnMaxSockCountUpdate;
    }

    private void Start()
    {
        sliders = new Slider[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
            sliders[i] = transform.GetChild(i).GetComponentInChildren<Slider>();


        OnMaxSockCountUpdate(PlayerController.instance.SockLauncher.simultaneousSocks);
        OnSockCountUpdate(PlayerController.instance.SockLauncher.GetCurrentSocks());
    }



    private void Update()
    {
        if (currentSlider)
        {
            currentSlider.value = PlayerController.instance.SockLauncher.CurrentSockCooldown();
        }
    }


    private void OnSockCountUpdate(int sockCount)
    {
        int idx = 0;

        foreach (Slider slider in sliders)
        {
            slider.value = idx < sockCount ? 1 : 0;

            idx++;
        }


        if (sockCount < sliders.Length)
        {
            currentSlider = sliders[sockCount];
        }
        else currentSlider = null;
    }

    private void OnMaxSockCountUpdate(int sockCount)
    {
        int idx = 0;

        foreach (Slider slider in sliders)
        {
            slider.gameObject.SetActive(idx < sockCount);

            idx++;
        }

    }


}
