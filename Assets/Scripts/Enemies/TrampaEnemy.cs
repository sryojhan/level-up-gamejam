using System.Collections;
using UnityEngine;

public class TrampaEnemy : BaseEnemy
{
    public float trapTime;
    public float cooldownTime;
    public GameObject model;

    private bool inAnimation;
    private float lastTimeSinceAttack;
    private BoxCollider2D ownBoxCollider2d;
    private Vector3 modelInitialPosition;

    void Start()
    {
        base.CustomStart();

        ownBoxCollider2d = GetComponent<BoxCollider2D>();
        inAnimation = false;
        lastTimeSinceAttack = cooldownTime;
        modelInitialPosition = model.transform.position;
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
        model.transform.position = model.transform.position + new Vector3(0, 1, 0);

        yield return new WaitForSeconds(time);

        collider2D.enabled = false;
        currentState = EnemyState.idle;
        inAnimation = false;
        lastTimeSinceAttack = 0;
        model.transform.position = modelInitialPosition;
    }
}
