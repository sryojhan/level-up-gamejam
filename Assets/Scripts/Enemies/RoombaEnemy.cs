using UnityEngine;

public class RoombaEnemy : BaseEnemy
{
    void Start()
    {
        base.CustomStart();
        currentState = EnemyState.attack;

        ownRigidbody.linearVelocity = ((Vector2)target.position - spawnPositionCoordinates) * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ownRigidbody.linearVelocity = ((Vector2)target.position - (Vector2)transform.position) * moveSpeed;
    }

    public override void OwnKnockback(Vector2 playerDirection, float force)
    {
        transform.position = (Vector2)transform.position + ((Vector2)transform.position - playerDirection).normalized * force;
    }
}
