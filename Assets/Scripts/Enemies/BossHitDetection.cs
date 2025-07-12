using UnityEngine;

public class BossHitDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponentInParent<BaseEnemy>().OnTriggerEnter2D(collision);
    }
}
