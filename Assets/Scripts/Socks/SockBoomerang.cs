using UnityEngine;

public class SockBoomerang : MonoBehaviour
{
    public Vector2 direction;

    public float throwForce = 1;
    public float rotationSpeed = 1;
    public float comebackForce = 1;

    private Rigidbody2D rb;
    private Transform child;

    private bool directionChanged = false;


    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        child = transform.GetChild(0);

        rb.AddForce(direction * throwForce, ForceMode2D.Impulse);

    }

    private void Update()
    {
        child.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }


    private void FixedUpdate()
    {
        rb.AddForce(-direction * comebackForce);


        if(!directionChanged && rb.linearVelocity.sqrMagnitude < 0.2)
        {
            directionChanged = true;

            direction = (transform.position - PlayerController.instance.transform.position).normalized;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BaseEnemy enemy = collision.gameObject.GetComponent<BaseEnemy>();

        if (enemy)
        {

            collision.gameObject.BroadcastMessage("OnHurt");
        }
    }


    public void OnPlayerCatch()
    {
        if (!directionChanged) return;

        Destroy(gameObject);
        PlayerController.instance.SockLauncher.RecoverSock();
    }

}

