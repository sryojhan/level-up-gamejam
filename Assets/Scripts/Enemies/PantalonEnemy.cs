using UnityEngine;

public class PantalonEnemy : BaseEnemy
{
    public float margin;

    void Start()
    {
        base.CustomStart();
        ownAnimator.Play("Atacado");

        if(enemyManager)
            enemyManager.AddEnemy();
    }

    void FixedUpdate()
    {
        CheckPlayerInRange();
        CheckIfIdle();
    }

    override public void CheckPlayerInRange()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, target.position);

        Vector2 direction;

        if (distanceToPlayer <= alertRadius)
        {
            direction = (target.position - transform.position).normalized;

            Vector2 force = direction * moveSpeed;

            ownRigidbody.AddForce(force);
            CheckRigidbodyVelocity(direction);

            float angle = Mathf.Atan2(direction.y, direction.x) * -Mathf.Rad2Deg;
            GetComponentInChildren<Rotate3DModels>().SetRotation(angle);

            ownAnimator.Play("Andar");
            ChangeState(EnemyState.attack);
        }
        else if (distanceToPlayer > alertRadius && currentState != EnemyState.idle) 
        {
            direction = (spawnPositionCoordinates - (Vector2)transform.position).normalized;

            Vector2 force = direction * moveSpeed;

            ownRigidbody.AddForce(force);
            CheckRigidbodyVelocity(direction);

            float angle = Mathf.Atan2(direction.y, direction.x) * -Mathf.Rad2Deg;
            GetComponentInChildren<Rotate3DModels>().SetRotation(angle);

            ownAnimator.Play("Andar");
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
            ownAnimator.Play("Atacado");
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

    public override void OwnKnockback()
    {
        ownRigidbody.linearVelocity = ((Vector2)transform.position - (Vector2)target.position).normalized * knockBackForce;
    }
}
