using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractableUI : Singleton<InteractableUI>
{
    public TextMeshProUGUI text;

    public CoroutineAnimation revealCoroutine;
    public CoroutineAnimation hideCoroutine;

    private void Start()
    {
        text.text = "";
    }
    public void Reveal(string message)
    {
        hideCoroutine.Stop(this);


        void OnUpdate(float i)
        {
            text.text = message[..Mathf.RoundToInt(i * message.Length)];
        }

        void OnEnd()
        {
            text.text = message;
        }

        revealCoroutine.Play(this, onUpdate: OnUpdate, onEnd: OnEnd);

    }

    public void Hide()
    {
        revealCoroutine.Stop(this);

        string initialString = text.text;

        void OnUpdate(float i)
        {
            i = 1 - i;

            text.text = initialString[..Mathf.RoundToInt(i * initialString.Length)];
        }

        void OnEnd()
        {
            text.text = "";
        }

        hideCoroutine.Play(this, onUpdate: OnUpdate, onEnd: OnEnd);
    }


}
