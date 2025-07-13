using TMPro;
using UnityEngine;


public class InGameControls : MonoBehaviour
{
    TextMeshProUGUI text;


    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        text.text = PlatformButtonTranslator.ProcessString(
            Localization.GetText("controls")
            );

    }
}
