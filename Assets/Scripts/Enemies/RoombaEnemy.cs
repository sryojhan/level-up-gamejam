using UnityEngine;

public class RoombaEnemy : BaseEnemy
{
    void Start()
    {
        base.CustomStart();
        currentState = EnemyState.attack;
        ownRigidbody.linearVelocity = ((Vector2)target.position - spawnPositionCoordinates).normalized * moveSpeed;

        if(enemyManager)
            enemyManager.AddEnemy();
    }

    public void RoombaCollision(Collision2D collision)
    {
        Vector2 dir = Vector2.zero;

        if(Vector2.Distance(spawnPositionCoordinates, target.position) > alertRadius)
        {
            dir = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized;
        }
        else
        {
            dir = ((Vector2)target.position - (Vector2)transform.position).normalized;
        }

        ownRigidbody.linearVelocity = dir * moveSpeed;

        float angle = Mathf.Atan2(dir.y, dir.x) * -Mathf.Rad2Deg;
        GetComponentInChildren<Rotate3DModels>().SetRotation(angle);
    }

    public override void OwnKnockback()
    {
        transform.position = (Vector2)transform.position + ((Vector2)transform.position - (Vector2)target.position).normalized * knockBackForce;
    }
}
