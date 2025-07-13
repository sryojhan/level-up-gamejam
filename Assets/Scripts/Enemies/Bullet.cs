using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int despawnDistance;
    public int aliveTime;

    private Vector2 direction;
    private float speed;
    private Vector2 spawnPosition;
    private Rigidbody2D ownRigidbody;
    private int bulletDamage;
    private float timeSinceAlive;

    public void Init(Vector2 _direction, Vector2 _position, float _speed, int damage)
    {
        this.direction = _direction.normalized;
        this.speed = _speed;
        this.bulletDamage = damage;
        spawnPosition = _position;

        ownRigidbody = GetComponent<Rigidbody2D>();
        ownRigidbody.linearVelocity = (direction * speed);

        transform.position = spawnPosition + direction * 1.5f;

        timeSinceAlive = 0;
    }

    void FixedUpdate()
    {
        CheckDestroy();
    }

    private void CheckDestroy()
    {
        if(Vector2.Distance(transform.position, spawnPosition) > despawnDistance || timeSinceAlive > aliveTime)
        {
            Destroy(gameObject);
        }
        else
        {
            timeSinceAlive += Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject != PlayerController.instance.gameObject) return;

        PlayerController.instance.PlayerHealth.LoseHealth();
        Destroy(gameObject);
    }

    public void OnHurt()
    {
        Destroy(gameObject);
    }
}
