using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractableUI : Singleton<InteractableUI>
{
    public TextMeshProUGUI text;

    public CoroutineAnimation revealCoroutine;
    public CoroutineAnimation hideCoroutine;


    public Vector2 hiddenPosition;
    public Vector2 visiblePosition;

    private void Start()
    {
        text.text = "";

        visiblePosition = text.rectTransform.anchoredPosition;
        text.rectTransform.anchoredPosition = hiddenPosition;
    }
    public void Reveal(string message)
    {
        revealCoroutine.Stop(this);
        hideCoroutine.Stop(this);

        text.rectTransform.anchoredPosition = hiddenPosition;
        text.text = message;

        revealCoroutine.Play(this, text.rectTransform, visiblePosition);
    }

    public void Hide()
    {
        revealCoroutine.Stop(this);
        hideCoroutine.Stop(this);

        hideCoroutine.Play(this, text.rectTransform, hiddenPosition);
    }


    [EasyButtons.Button]
    public void SaveHiddenPosition()
    {
        hiddenPosition = text.rectTransform.anchoredPosition;
    }
}
