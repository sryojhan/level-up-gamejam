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
        
        Camera.main.GetComponent<SimpleShake>().Shake();

        collision.gameObject.BroadcastMessage("OnHurt");
    }
}
