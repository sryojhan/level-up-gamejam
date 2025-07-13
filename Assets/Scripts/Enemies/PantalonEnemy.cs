using System;
using UnityEngine;

public class PantalonEnemy : BaseEnemy
{
    public float leaveRadiusOnAttackMultiplier = 1.5f;

    public float steeringStrength = 1;

    void Start()
    {
        base.CustomStart();
        ownAnimator.Play("Atacado");

        if (enemyManager)
            enemyManager.AddEnemy();
    }

    void FixedUpdate()
    {
        ManageStateLogic();
        CheckIfIdle();
    }

    private void Update()
    {
        
    }


    override public void ManageStateLogic()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, target.position);
        float distanceToSpawn = (spawnPositionCoordinates - (Vector2)transform.position).magnitude;

        float playerDistanceToSpawn = Vector2.Distance(spawnPositionCoordinates, target.position);


        switch (currentState)
        {
            case EnemyState.idle:

                if(playerDistanceToSpawn <= alertRadius)
                {
                    //Begin attack
                    ChangeState(EnemyState.attack);
                }

                break;

            case EnemyState.walk:

                if (playerDistanceToSpawn <= alertRadius)
                {
                    //Begin attack
                    ChangeState(EnemyState.attack);
                }

                else if(distanceToSpawn <= 0.4f)
                {
                    print("set idle");
                    ChangeState(EnemyState.idle);
                }


            break;

            case EnemyState.attack:

                if(distanceToSpawn > alertRadius * leaveRadiusOnAttackMultiplier)
                {
                    ChangeState(EnemyState.walk);
                }

                break;
            default:
                break;
        }

        if(currentState == EnemyState.idle)
        {
            ownRigidbody.linearVelocity = Vector2.zero;
            ownAnimator.Play("Atacado");
            return;
        }

        Vector2 direction = Vector2.zero;

        switch (currentState)
        {
            case EnemyState.walk:

                direction = (spawnPositionCoordinates - (Vector2)transform.position).normalized;

                break;
            case EnemyState.attack:

                direction = (target.position - transform.position).normalized;

                break;
            default:
                break;
        }

        Vector2 targetSpeed = direction * moveSpeed;

        Vector2 steering = targetSpeed - ownRigidbody.linearVelocity;

        ownRigidbody.AddForce(steering * steeringStrength);
        CheckRigidbodyVelocity(direction);

        float angle = Mathf.Atan2(direction.y, direction.x) * -Mathf.Rad2Deg;
        GetComponentInChildren<Rotate3DModels>().SetRotation(angle);

        ownAnimator.Play("Andar");
    }

    public override void CheckIfIdle()
    {
        if (currentState != EnemyState.walk) return;

        float distanceToSpawn = (spawnPositionCoordinates - (Vector2)transform.position).sqrMagnitude;
        if (distanceToSpawn <= 0.1f)
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

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Application.isPlaying ? spawnPositionCoordinates : transform.position, alertRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(Application.isPlaying ? spawnPositionCoordinates : transform.position, alertRadius * leaveRadiusOnAttackMultiplier);

    }
#endif
}
