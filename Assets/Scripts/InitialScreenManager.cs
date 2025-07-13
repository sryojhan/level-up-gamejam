using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InitialScreenManager : MonoBehaviour
{
    public void SelectSpanish()
    {
        Localization.instance.currentLanguage = Localization.Language.Spanish;
    }

    public void SelectEnglish()
    {
        Localization.instance.currentLanguage = Localization.Language.English;
    }

    public void SelectCatalonian()
    {
        Localization.instance.currentLanguage = Localization.Language.Catalonian;
    }


    public CanvasGroup alpha;

    bool begin = false;

    private void BeginGame()
    {
        if (begin) return;
        begin = true;

        StartCoroutine(Animation());
    }


    private IEnumerator Animation()
    {

    }

}
