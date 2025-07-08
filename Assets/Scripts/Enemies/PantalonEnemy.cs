using UnityEngine;

public class PantalonEnemy : BaseEnemy
{
    public float margin;

    void Start()
    {
        currentState = EnemyState.idle;
        ownRigidbody = GetComponent<Rigidbody2D>();
        target = PlayerController.instance.transform;
        spawnPositionCoordinates = transform.position;
    }

    void FixedUpdate()
    {
        CheckPlayerInRange();
        CheckIfIdle();
    }

    override public void CheckPlayerInRange()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, target.position);
        if (distanceToPlayer <= chaseRadius)
        {
            Vector2 direction = (target.position - transform.position).normalized;

            Vector2 force = direction * moveSpeed;

            ownRigidbody.AddForce(force);

            CheckRigidbodyVelocity(direction);

            ChangeState(EnemyState.attack);
        }
        else if (distanceToPlayer > chaseRadius && currentState != EnemyState.idle) 
        {
            Vector2 direction = (spawnPositionCoordinates - (Vector2)transform.position).normalized;

            Vector2 force = direction * moveSpeed;

            ownRigidbody.AddForce(force);

            CheckRigidbodyVelocity(direction);

            ChangeState(EnemyState.walk);
        }
    }

    public override void CheckIfIdle()
    {
        if (currentState == EnemyState.attack) return;

        float distanceToSpawn = (spawnPositionCoordinates - (Vector2)transform.position).sqrMagnitude;
        if (distanceToSpawn <= margin * margin)
        { 
            ownRigidbody.linearVelocity = Vector2.zero;
            ChangeState(EnemyState.idle);
        }
    }

    private void CheckRigidbodyVelocity(Vector2 direction)
    {
        if (ownRigidbody.linearVelocity.sqrMagnitude > moveSpeed * moveSpeed)
        {
            ownRigidbody.linearVelocity = moveSpeed * direction;
        }
    }

    public override void OwnKnockback(Vector2 playerDirection, float force)
    {
        ownRigidbody.linearVelocity = ((Vector2)transform.position - playerDirection).normalized * force;
    }
}
