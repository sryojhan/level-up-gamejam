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



    private void Start()
    {
        dialogueParent.SetActive(false);
    }


    public void BeginDialogue(DialogueContent dialogue, OnDialogueEnd onEnd)
    {
        dialogueParent.SetActive(true);
        StartCoroutine(DialogueCoroutine(dialogue, onEnd));
    }

    private IEnumerator DialogueCoroutine(DialogueContent dialogue, OnDialogueEnd onEnd)
    {
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
        }

        dialogueContent.text = "";


        //Initialise dialogue animation setup
        RevealDialogueAnimation(hasSpeaker);
        while (!IsRevealAnimationComplete()) yield return null;



        int currentDialoguePage = 0;

        yield return new WaitForSeconds(.2f);

        while (currentDialoguePage < dialogue.content.Length)
        {
            int textLength = dialogue.content[currentDialoguePage].Length;
            float duration = textLength * revealDurationPerChar;
            float speed = 1.0f / duration;


            bool skipped = false;

            for (float i = 0; i < 1 && !skipped; i += Time.deltaTime * speed)
            {
                float c = textRevealInterpolation.Interpolate(i);

                dialogueContent.text = dialogue.content[currentDialoguePage][..Mathf.FloorToInt(c * textLength)];

                if (PlayerController.instance.InputManager.WantsToInteract())
                {
                    skipped = true;
                    dialogueContent.text = dialogue.content[currentDialoguePage];
                }

                yield return null;
            }

            if (!skipped)
            {
                dialogueContent.text = dialogue.content[currentDialoguePage];
                yield return new WaitForSeconds(.2f);
            }

            while (!PlayerController.instance.InputManager.WantsToInteract())
            {
                yield return null;
            }

            currentDialoguePage++;
            yield return null;
        }


        //Hide animation
        HideDialogueAnimation(hasSpeaker);
        while (!IsHideAnimationComplete()) yield return null;


        onEnd?.Invoke();
        dialogueParent.SetActive(false);

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


    IEnumerator HighlightElement()
    {
        while (true)
        {

            yield return null;
        }
    }

}
