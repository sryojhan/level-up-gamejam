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

    public Sprite z1;
    public Sprite z2;
    public Sprite z3;

    public float timeBetweenZs = .2f;

    void OnDialogueEnd()
    {
        Invoke(nameof(ChangeFrame), changeFrameTime);
        Invoke(nameof(BeginZ1), beginZZZTime);
        Invoke(nameof(BeginZ2), beginZZZTime + timeBetweenZs);
        Invoke(nameof(BeginZ3), beginZZZTime + timeBetweenZs * 2);
        Invoke(nameof(ChangeScene), changeSceneTime);
    }

    void ChangeFrame()
    {
        backgroundImage.sprite = secondFrame;
    }

    void BeginZ1()
    {
        backgroundImage.sprite = z1;
    }

    void BeginZ2()
    {
        backgroundImage.sprite = z2;
    }

    void BeginZ3()
    {
        backgroundImage.sprite = z3;
    }


    void ChangeScene()
    {
        SceneTransition.SceneTransitionManager.instance.ChangeScene("Initial room");
    }
}
