using UnityEngine;

public enum EnemyState
{
    idle, 
    walk,
    attack,
    stagger, 
    cooldown
}
public class BaseEnemy : MonoBehaviour
{
    //Enemy variables
    public EnemyState currentState;
    public int health;
    public string enemyName;
    public int baseAttack;
    public float alertRadius;
    public float moveSpeed;
    public float knockBackForce;

    protected Rigidbody2D ownRigidbody;
    protected Transform target;
    protected Vector2 spawnPositionCoordinates;
    protected Animator ownAnimator;
    protected EnemyManager enemyManager;
    protected bool hasDied;

    protected bool isBoss;
    public virtual void CustomStart()
    {
        currentState = EnemyState.idle;
        ownRigidbody = GetComponent<Rigidbody2D>();
        target = PlayerController.instance.transform;
        spawnPositionCoordinates = transform.position;
        ownAnimator = GetComponentInChildren<Animator>();
        enemyManager = EnemyManager.instance;
        hasDied = false;
        isBoss = false;
    }
    public virtual void CheckPlayerInRange() { }
    public virtual void CheckIfIdle() { }

    public virtual void OwnKnockback() { }

    public void OnHurt()
    {
        TakeDamage(1);
        OwnKnockback();
    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject != PlayerController.instance.gameObject) return;

        PlayerController.instance.PlayerHealth.LoseHealth();
    }

    public virtual void TakeDamage(int damage) 
    {
        if (hasDied) return;
        if (ownAnimator != null)
        {
            ownAnimator.Play("Atacado");
        }
        health -= damage;
        if (health <= 0)
        {
            hasDied = true;
            Destroy(gameObject, 0.5f);
            enemyManager.EnemyDied();
        }
    }
    public void ChangeState(EnemyState state)
    {
        if (currentState != state)
        {
            currentState = state;
        }
    }


}
