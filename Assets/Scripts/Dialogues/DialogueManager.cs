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
    public CoroutineAnimation revealDialogueBackground;
    public Vector2 backgroundAnimationOrigin;

    [Header("Speaker")]
    public TextMeshProUGUI dialogueSpeaker;
    public Image speakerTextBackground;
    public CoroutineAnimation revealSpeaker;

    [Header("Npc sprite")]
    public Image speakerSprite;
    public Transform npcPivot;
    public CoroutineAnimation revealNpc;

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

            for (float i = 0; i < 1; i += Time.deltaTime * speed)
            {
                float c = textRevealInterpolation.Interpolate(i);

                dialogueContent.text = dialogue.content[currentDialoguePage][..Mathf.FloorToInt(c * textLength)];

                if (PlayerController.instance.InputManager.WantsToInteract())
                {
                    break;
                }

                yield return null;
            }

            yield return new WaitForSeconds(.2f);

            dialogueContent.text = dialogue.content[currentDialoguePage];

            while (!PlayerController.instance.InputManager.WantsToInteract())
            {
                yield return null;
            }

            currentDialoguePage++;
            yield return null;
        }


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


    private bool IsRevealAnimationComplete()
    {
        return revealNpc.IsFinished() && revealDialogueBackground.IsFinished() && revealSpeaker.IsFinished();
    }


    IEnumerator HighlightElement()
    {
        while (true)
        {

            yield return null;
        }
    }

}
