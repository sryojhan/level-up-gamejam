using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InitialScreenManager : MonoBehaviour
{
    public void SelectSpanish()
    {
        Localization.instance.currentLanguage = Localization.Language.Spanish;

        BeginGame();
    }

    public void SelectEnglish()
    {
        Localization.instance.currentLanguage = Localization.Language.English;
        BeginGame();
    }

    public void SelectCatalonian()
    {
        Localization.instance.currentLanguage = Localization.Language.Catalonian;
        BeginGame();
    }


    public CanvasGroup alpha;

    bool begin = false;

    private void BeginGame()
    {
        if (begin) return;
        begin = true;


        zzz.gameObject.SetActive(false);

        StartCoroutine(Animation());
    }

    public CoroutineAnimation revealAnimation;

    private IEnumerator Animation()
    {
        void Reveal(float i)
        {
            alpha.alpha = Mathf.Lerp(1, 0, i);
        }

        revealAnimation.Play(this, onUpdate: Reveal);

        while (!revealAnimation.IsFinished()) yield return null;


        DialogueManager.instance.BeginDialogue(dialogue, OnDialogueEnd);
    }

    public DialogueContent dialogue;

    public float changeFrameTime = 1f;
    public float beginZZZTime = 2f;
    public float changeSceneTime = 3f;

    public Image backgroundImage;
    public Sprite secondFrame;

    public CoroutineAnimation revealZZZ;
    public Image zzz;

    void OnDialogueEnd()
    {
        Invoke(nameof(ChangeFrame), changeFrameTime);
        Invoke(nameof(BeginZZZ), beginZZZTime);
        Invoke(nameof(ChangeScene), changeSceneTime);
    }

    void ChangeFrame()
    {
        backgroundImage.sprite = secondFrame;
    }

    void BeginZZZ()
    {
        zzz.gameObject.SetActive(true);

        void Reveal(float i)
        {
            zzz.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, i));
        }

        revealAnimation.Play(this, onUpdate: Reveal);
    }

    void ChangeScene()
    {
        SceneTransition.SceneTransitionManager.instance.ChangeScene("Initial room");
    }
}
