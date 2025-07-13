using UnityEngine;


[RequireComponent(typeof(UID))]
public class GameEndCheck : MonoBehaviour
{
    public GameObject[] laundryBaskets;
    private bool canEndGame = true;

    public DialogueContent gameEndConfirmation;
    public DialogueContent notPossible;


    public Interactable door;

    private bool hasTheWarningBeenGiven = false;

    public float shakeDuration = 3;
    public float shakeStrength = 3;

    private string uid;
    private void Start()
    {
        uid = GetComponent<UID>().uid;

        if(PersistentData.Get(uid) != 0)
        {
            Destroy(door);
            return;
        }

        for (int i = 0; i < 3; i++)
        {
            bool laundry = PersistentData.collectedLaundry[i];
            canEndGame &= laundry;
            laundryBaskets[i].SetActive(laundry);
        }


    }


    public void TryEndGame()
    {
        if (canEndGame)
        {
            EndGame();
        }
        else
        {
            NotPossibleToEndGame();
        }
    }

    private void EndGame()
    {
        if (!hasTheWarningBeenGiven)
        {
            hasTheWarningBeenGiven = true;
            DialogueManager.instance.BeginDialogue(gameEndConfirmation, door.EndInteraction);
        }
        else
        {
            Camera.main.GetComponent<SimpleShake>().ShakeCustom(3, 3);
            Invoke(nameof(DestroyDoor), 3);
        }
    }


    void DestroyDoor()
    {
        Destroy(door.gameObject);
        PersistentData.Set(uid, 1);

        door.EndInteraction();
    }


    private void NotPossibleToEndGame()
    {
        DialogueManager.instance.BeginDialogue(notPossible, door.EndInteraction);
    }


    [EasyButtons.Button]
    public void DEBUG_OptainAllBaskets()
    {
        PersistentData.collectedLaundry[0] = true;
        PersistentData.collectedLaundry[1] = true;
        PersistentData.collectedLaundry[2] = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneTransition.SceneTransitionManager.instance.ChangeScene("BossRoom");
    }
}
