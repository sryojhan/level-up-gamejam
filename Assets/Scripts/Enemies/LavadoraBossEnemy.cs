using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class LavadoraBossEnemy : BaseEnemy
{
    public List<string> animations;
    public Animator[] animatorList;
    public float attackCooldown;

    private int animationListCount;
    private float timeSinceLastAttack;
    void Start()
    {
        base.CustomStart();

        //El padre
        ownAnimator = animatorList[0];

        //animator[1] es el cuerpo de la lavadora
        animatorList[1].Play("Take 001");

        ShuffleStringList(animations);
        animationListCount = 0;
        timeSinceLastAttack = attackCooldown;
    }

    void FixedUpdate()
    {
        if (!ownAnimator.GetCurrentAnimatorStateInfo(0).IsName("Default")) return;

        if (animations.Count == 0) return;

        if(timeSinceLastAttack >= attackCooldown)
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        ownAnimator.Play(animations[animationListCount]);
        animationListCount++;
        if (animationListCount == animations.Count)
        {
            animationListCount = 0;
        }
    }

    public void ShuffleStringList(List<string> list)
    {  
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            string value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public override void TakeDamage(int damage)
    {
        if (hasDied) return;
        if (animatorList[1] != null)
        {
            animatorList[1].Play("Take 001");
        }
        health -= damage;
        if (health <= 0)
        {
            hasDied = true;
            Destroy(gameObject, 0.5f);
            SceneTransition.SceneTransitionManager.instance.ChangeScene("VictoryScreen");
        }
    }
}
