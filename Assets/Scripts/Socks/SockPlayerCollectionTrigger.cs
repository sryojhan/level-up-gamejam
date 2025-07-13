using UnityEngine;

public class SockPlayerCollectionTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerController.instance.gameObject)
        {
            GetComponentInParent<SockBoomerang>().OnPlayerCatch();
        }
    }
}
