using UnityEngine;

public enum EnemyState
{
    idle, 
    walk,
    attack,
    stagger
}
public class BaseEnemy : MonoBehaviour
{
    //Enemy variables
    public EnemyState currentState;
    public int health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public float chaseRadius;
    protected Rigidbody2D ownRigidbody;

    //Enemy position references
    protected Transform target;
    protected Vector2 spawnPositionCoordinates;

    public virtual void CheckPlayerInRange() { }
    public virtual void CheckIfIdle() { }
    public virtual void TakeDamage(int damage) { }
    public virtual void OwnKnockback(Vector2 playerDirection, float force) { }
    public void ChangeState(EnemyState state)
    {
        if (currentState != state)
        {
            currentState = state;
        }
    }
}
