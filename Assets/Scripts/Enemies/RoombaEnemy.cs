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
        Vector2 dir = ((Vector2)target.position - (Vector2)transform.position);
        ownRigidbody.linearVelocity = dir * moveSpeed;

        float angle = Mathf.Atan2(dir.y, dir.x) * -Mathf.Rad2Deg;
        GetComponentInChildren<Rotate3DModels>().SetRotation(angle);
    }

    public override void OwnKnockback(Vector2 playerDirection, float force)
    {
        transform.position = (Vector2)transform.position + ((Vector2)transform.position - playerDirection).normalized * force;
    }
}
