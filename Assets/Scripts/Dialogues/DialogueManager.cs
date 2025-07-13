using System.Collections;

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class DialogueManager : Singleton<DialogueManager>
{
    public delegate void OnDialogueEnd();

    public GameObject dialogueParent;

    [Header("Main content")]
    public Image background;
    public TextMeshProUGUI dialogueContent;
    public Vector2 backgroundAnimationOrigin;
    public CoroutineAnimation revealDialogueBackground;
    public CoroutineAnimation hideDialogueBackground;

    [Header("Speaker")]
    public TextMeshProUGUI dialogueSpeaker;
    public Image speakerTextBackground;
    public CoroutineAnimation revealSpeaker;
    public CoroutineAnimation hideSpeaker;

    [Header("Npc sprite")]
    public Image speakerSprite;
    public Transform npcPivot;
    public CoroutineAnimation revealNpc;
    public CoroutineAnimation hideNpc;

    [Header("Text reveal")]
    public Interpolation textRevealInterpolation;
    public float revealDurationPerChar = 0.01f;


    [Header("Highlight")]
    public Image highlightElement;
    public Image highlightBackground;

    public float maxHighlightElementScale = 2;

    public float minHighlightAlphaMask = 0.3f;
    public CoroutineAnimation alphaMaskCoroutine;
    public CoroutineAnimation elementRevealAnimation;
    public CoroutineAnimation elementHideAnimation;
    private Coroutine highlightCoroutineReference;


    private bool dialogueInCourse = false;

    private void Start()
    {
        dialogueParent.SetActive(false);
    }


    public void BeginDialogue(DialogueContent dialogue, OnDialogueEnd onEnd, Sprite highlightSprite = null)
    {
        highlightElement.gameObject.SetActive(highlightSprite != null);

        dialogueParent.SetActive(true);
        StartCoroutine(DialogueCoroutine(dialogue, onEnd));

        if (highlightSprite != null)
        {
            highlightCoroutineReference = StartCoroutine(HighlightElement(highlightSprite));
        }
    }

    private IEnumerator DialogueCoroutine(DialogueContent dialogue, OnDialogueEnd onEnd)
    {
        dialogueInCourse = true;

        bool hasSpeaker = !string.IsNullOrEmpty(dialogue.speaker);

        if (!hasSpeaker)
        {
            dialogueSpeaker.text = "";
            speakerSprite.enabled = false;
            speakerTextBackground.enabled = false;
        }
        else
        {
            speakerSprite.enabled = dialogue.speakerSprite ? true : false;
            speakerTextBackground.enabled = true;

            dialogueSpeaker.text = dialogue.speaker;
            speakerSprite.sprite = dialogue.speakerSprite;
            speakerSprite.GetComponent<ImageMantainAspectRatio>().UpdateImageBasedOnSpriteAspectRatio();
        }

        dialogueContent.text = "";


        //Initialise dialogue animation setup
        RevealDialogueAnimation(hasSpeaker);
        while (!IsRevealAnimationComplete()) yield return null;



        int currentDialoguePage = 0;

        yield return new WaitForSeconds(.2f);


        while (currentDialoguePage < dialogue.content.Length)
        {
            SoundManager.instance.BeginDialogue();

            string localizedText = Localization.GetText(dialogue.content[currentDialoguePage]);


            int textLength = localizedText.Length;
            float duration = textLength * revealDurationPerChar;
            float speed = 1.0f / duration;


            bool skipped = false;

            for (float i = 0; i < 1 && !skipped; i += Time.deltaTime * speed)
            {
                float c = textRevealInterpolation.Interpolate(i);

                dialogueContent.text = localizedText[..Mathf.FloorToInt(c * textLength)];

                if (!PlayerController.instance.InputManager.WantsToInteract() && !PlayerController.instance.InputManager.WantsToAttack())
                {
                    skipped = true;
                    dialogueContent.text = localizedText;
                }

                yield return null;
            }

            if (!skipped)
            {
                dialogueContent.text = localizedText;
                //yield return new WaitForSeconds(.2f);
            }

            SoundManager.instance.EndDialogue();

            while (!PlayerController.instance.InputManager.WantsToInteract() && !PlayerController.instance.InputManager.WantsToAttack())
            {
                yield return null;
            }

            currentDialoguePage++;
            yield return null;
        }

        dialogueInCourse = false;

        //Hide animation
        HideDialogueAnimation(hasSpeaker);
        while (!IsHideAnimationComplete()) yield return null;


        onEnd?.Invoke();
        dialogueParent.SetActive(false);

        if (highlightCoroutineReference != null)
            StopCoroutine(highlightCoroutineReference);
    }


    private void RevealDialogueAnimation(bool hasSpeaker)
    {
        void RevealNpc(float i)
        {
            Quaternion A = Quaternion.Euler(0, 0, 179);
            Quaternion B = Quaternion.identity;

            npcPivot.localRotation = Quaternion.LerpUnclamped(A, B, i);
        }

        if (hasSpeaker)
        {
            RevealNpc(0);
            revealNpc.Play(this, onUpdate: RevealNpc);
        }

        background.gameObject.SetActive(true);

        Vector2 destination = background.rectTransform.anchoredPosition;
        background.rectTransform.anchoredPosition = backgroundAnimationOrigin;
        revealDialogueBackground.Play(this, background.rectTransform, destination);

        void RevealSpeaker(float i)
        {
            speakerTextBackground.transform.localScale = Vector2.one * i;
        }

        if (hasSpeaker)
        {
            RevealSpeaker(0);
            revealSpeaker.Play(this, onUpdate: RevealSpeaker);
        }

    }


    private void HideDialogueAnimation(bool hasSpeaker)
    {
        void HideNpc(float i)
        {
            Quaternion A = Quaternion.identity;
            Quaternion B = Quaternion.Euler(0, 0, 181);

            npcPivot.localRotation = Quaternion.LerpUnclamped(A, B, i);
        }

        if (hasSpeaker)
        {
            hideNpc.Play(this, onUpdate: HideNpc);
        }


        float previousDelay = hideDialogueBackground.delay;

        if (!hasSpeaker)
        {
            print("removed delay");
            hideDialogueBackground.delay = 0;
        }

        Vector2 currentPosition = background.rectTransform.anchoredPosition;

        void OnBackgroundEnd()
        {
            background.gameObject.SetActive(false);
            background.rectTransform.anchoredPosition = currentPosition;

            hideDialogueBackground.delay = previousDelay;
        }


        hideDialogueBackground.Play(this, background.rectTransform, backgroundAnimationOrigin, onEnd: OnBackgroundEnd);

        void RevealSpeaker(float i)
        {
            speakerTextBackground.transform.localScale = Vector2.one * Mathf.Lerp(1, 0, i);
        }



        if (hasSpeaker)
        {
            hideSpeaker.Play(this, onUpdate: RevealSpeaker);
        }

    }



    private bool IsRevealAnimationComplete()
    {
        return revealNpc.IsFinished() && revealDialogueBackground.IsFinished() && revealSpeaker.IsFinished();
    }

    private bool IsHideAnimationComplete()
    {
        return hideNpc.IsFinished() && hideDialogueBackground.IsFinished() && hideSpeaker.IsFinished();
    }


    IEnumerator HighlightElement(Sprite highlightSprite)
    {
        highlightElement.transform.localScale = Vector3.zero;
        highlightElement.sprite = highlightSprite;

        highlightElement.GetComponent<ImageMantainAspectRatio>().UpdateImageBasedOnSpriteAspectRatio();

        highlightBackground.material.SetFloat("_AlphaMask", 1);
        void OnUpdateReveal(float i)
        {
            float value = Mathf.LerpUnclamped(1, minHighlightAlphaMask, i);

            highlightBackground.material.SetFloat("_AlphaMask", value);
        }

        alphaMaskCoroutine.Play(this, onUpdate: OnUpdateReveal);


        void RevealHighlightElementUpdate(float i)
        {
            highlightElement.transform.localScale = Mathf.LerpUnclamped(0, maxHighlightElementScale, i) * Vector3.one;
        }

        elementRevealAnimation.Play(this, onUpdate: RevealHighlightElementUpdate);

        while (!alphaMaskCoroutine.IsFinished()) yield return null;

        while (dialogueInCourse)
        {
            yield return null;
        }

        elementRevealAnimation.Stop(this);

        void OnUpdateHide(float i)
        {
            float value = Mathf.LerpUnclamped(minHighlightAlphaMask, 1, i);

            highlightBackground.material.SetFloat("_AlphaMask", value);
        }

        highlightBackground.material.SetFloat("_AlphaMask", 1);


        float initialScale = highlightElement.transform.localScale.x;

        void HideHighlightElementUpdate(float i)
        {
            highlightElement.transform.localScale = Mathf.LerpUnclamped(initialScale, 0, i) * Vector3.one;
        }

        elementHideAnimation.Play(this, onUpdate: HideHighlightElementUpdate);



        alphaMaskCoroutine.Play(this, onUpdate: OnUpdateHide);
        while (!alphaMaskCoroutine.IsFinished()) yield return null;

        highlightCoroutineReference = null;
    }

}
