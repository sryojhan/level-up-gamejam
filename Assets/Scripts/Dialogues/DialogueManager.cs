using System.Collections;

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class DialogueManager : Singleton<DialogueManager>
{
    public GameObject dialogueParent;

    public TextMeshProUGUI dialogueSpeaker;
    public Image speakerSprite;
    public Image speakerTextBackground;

    public TextMeshProUGUI dialogueContent;

    public delegate void OnDialogueEnd();

    public Interpolation textRevealInterpolation;

    public float revealDurationPerChar = 0.01f;


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

        if (string.IsNullOrEmpty(dialogue.speaker))
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

}
