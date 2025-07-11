using UnityEngine;

public class PistolaCamisetaEnemy : BaseEnemy
{
    public GameObject bulletPrefab;
    public float attackSpeed;
    public float bulletSpeed;

    private float lastTimeSinceAttack;

    void Start()
    {
        base.CustomStart();
        lastTimeSinceAttack = attackSpeed;
        enemyManager.AddEnemy();
    }

    void FixedUpdate()
    {
        CheckPlayerInRange();
    }

    public override void CheckPlayerInRange()
    {
        if(Vector2.Distance(transform.position, target.position) < alertRadius)
        {

            Vector2 direction = target.position - transform.position;

            if (direction.magnitude > 0.1f)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * -Mathf.Rad2Deg;
                GetComponentInChildren<Rotate3DModels>().SetRotation(angle);
            }


            if (lastTimeSinceAttack < attackSpeed)
            {
                ChangeState(EnemyState.cooldown);
                lastTimeSinceAttack += Time.deltaTime;
            }
            else
            {
                lastTimeSinceAttack = 0;

                GameObject bullet = Instantiate(bulletPrefab);
                bullet.SetActive(true);
                bullet.GetComponent<Bullet>().Init((Vector2)target.position - spawnPositionCoordinates, transform.position, bulletSpeed, baseAttack);

                ChangeState(EnemyState.attack);
            }
        }
        else
        {
            ChangeState(EnemyState.idle);
        }
    }
}
