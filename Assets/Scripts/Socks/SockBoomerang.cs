using UnityEngine;

public class SockBoomerang : MonoBehaviour
{
    public Vector2 direction;

    public float force = 1;
    public float rotationSpeed = 1;
    public float comebackForce = 1;

    private Rigidbody2D rb;
    private Transform child;

    private Vector2 originPosition;

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        child = transform.GetChild(0);

        rb.AddForce(direction * force, ForceMode2D.Impulse);

        originPosition = transform.position;
    }

    private void Update()
    {
        child.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));

        rb.AddForce(-direction * comebackForce);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);


        BaseEnemy enemy = collision.gameObject.GetComponent<BaseEnemy>();

        if (enemy) {

            collision.gameObject.BroadcastMessage("OnHurt");
        }
    }
}

