using System.Collections;
using UnityEngine;

public class TrampaEnemy : BaseEnemy
{
    public float trapTime;
    public float cooldownTime;

    private bool inAnimation;
    private float lastTimeSinceAttack;
    private BoxCollider2D ownBoxCollider2d;

    void Start()
    {
        base.CustomStart();

        ownBoxCollider2d = GetComponent<BoxCollider2D>();
        inAnimation = false;
        lastTimeSinceAttack = cooldownTime;
    }
    void FixedUpdate()
    {
        CheckPlayerInRange();
    }

    public override void CheckPlayerInRange()
    {
        if (inAnimation) return;

        if(lastTimeSinceAttack < cooldownTime)
        {
            ChangeState(EnemyState.cooldown);
            lastTimeSinceAttack += Time.deltaTime;
        }
        else
        {
            if (Vector2.Distance(transform.position, target.position) < alertRadius)
            {
                StartCoroutine(AttackCoroutine(trapTime, ownBoxCollider2d));
            }
        }
    }

    public override void TakeDamage(int damage)
    {
        //Es una trampa no se muere jaja
    }

    private IEnumerator AttackCoroutine(float time, BoxCollider2D collider2D)
    {
        collider2D.enabled = true;
        currentState = EnemyState.attack;
        inAnimation = true; 

        yield return new WaitForSeconds(time);

        collider2D.enabled = false;
        currentState = EnemyState.idle;
        inAnimation = false;
        lastTimeSinceAttack = 0;
    }
}
