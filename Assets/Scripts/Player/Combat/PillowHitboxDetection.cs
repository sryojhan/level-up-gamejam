using UnityEngine;
using UnityEngine.Events;

public class PillowHitboxDetection : MonoBehaviour
{

    public UnityEvent onHit;


    public void ResetDetection()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onHit?.Invoke();
        collision.gameObject.BroadcastMessage("OnHurt");
    }
}
