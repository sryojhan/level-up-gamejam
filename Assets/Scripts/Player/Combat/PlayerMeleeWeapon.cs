using UnityEngine;

public class PlayerMeleeWeapon : MonoBehaviour
{
    public GameObject pillowPivot;

    private bool isAttaking = false;
    public float attackDuration = 1;

    private float attackTimer = 0;
    private bool lastAttackDirection = false;

    public Quaternion minRotation;
    public Quaternion maxRotation;

    public Interpolation interpolation;

    public Vector2 handHorizontalPosition;
    public Vector2 handVerticalPosition;

    public float anticipationTime = 0.2f;
    public float recoveryTime = 0.2f;

    public Sprite pillowRest;
    public Sprite pillowAttack;

    private SpriteRenderer pillowSpriteRenderer;

    public PillowHitboxDetection pillowHitDetection;
    public BoxCollider2D pillowCollider;

    public float freezeFrameDuration = .2f;
    private float freezeFrameTimer = 0;

    private void Start()
    {
        pillowPivot.SetActive(false);

        pillowSpriteRenderer = pillowPivot.GetComponentInChildren<SpriteRenderer>();
        pillowHitDetection.onHit.AddListener(OnHit);

        pillowPivot.transform.localRotation = maxRotation;
    }

    public void BeginAttack(bool attackButtonPressed, PlayerDirection direction)
    {
        if (isAttaking) return;

        if (attackButtonPressed)
        {
            isAttaking = true;

            Vector2 attackDirection = direction.GetVectorDirection();

            pillowPivot.transform.parent.localRotation = Quaternion.Euler(0, 0, Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg);

            switch (direction.direction)
            {
                case PlayerDirection.Direction.Down:
                    pillowPivot.transform.parent.localPosition = handVerticalPosition;
                    break;
                case PlayerDirection.Direction.Left:
                    pillowPivot.transform.parent.localPosition = new Vector2(-handHorizontalPosition.x, handHorizontalPosition.y);
                    break;
                case PlayerDirection.Direction.Up:
                    pillowPivot.transform.parent.localPosition = new Vector2(handVerticalPosition.x, -handVerticalPosition.y);
                    break;
                case PlayerDirection.Direction.Right:
                    pillowPivot.transform.parent.localPosition = handHorizontalPosition;
                    break;
            }


            ChangeState(AttackState.Anticipation);


            lastAttackDirection = !lastAttackDirection;

            pillowSpriteRenderer.sprite = pillowRest;

            pillowCollider.enabled = false;

            pillowPivot.SetActive(true);

        }
    }


    private enum AttackState
    {
        Anticipation, Attack, Recovery
    }

    AttackState attackState;


    private void ChangeState(AttackState newState)
    {
        attackState = newState;
        attackTimer = 0;
    }

    public void UpdateAttack()
    {
        if (!isAttaking) return;


        switch (attackState)
        {
            case AttackState.Anticipation:
                ManageAnticipation();
                break;
            case AttackState.Attack:
                ManageAttackMovement();
                break;
            case AttackState.Recovery:
                ManageRecovery();
                break;
            default:
                break;
        }

    }

    private float UpdateTimerState(float maxValue)
    {
        attackTimer += Time.deltaTime;

        return attackTimer / maxValue;
    }

    private void ManageAnticipation()
    {
        float i = UpdateTimerState(anticipationTime);


        if(i >= 1) //End
        {
            ChangeState(AttackState.Attack);
            pillowSpriteRenderer.sprite = pillowAttack;

            pillowCollider.enabled = true;
        }
    }
    private void ManageAttackMovement()
    {
        if (duringFreezeFrame)
        {
            freezeFrameTimer += Time.deltaTime;

            if(freezeFrameTimer > freezeFrameDuration)
            {
                duringFreezeFrame = false;
            }

            return;
        }



        float i = UpdateTimerState(attackDuration);

        Quaternion A = lastAttackDirection ? maxRotation : minRotation;
        Quaternion B = lastAttackDirection ? minRotation : maxRotation;

        pillowPivot.transform.localRotation = Quaternion.LerpUnclamped(A, B, interpolation.Interpolate(i));

        if (i >= 1) //Movement attack
        {
            ChangeState(AttackState.Recovery);
            pillowSpriteRenderer.sprite = pillowRest;

            pillowCollider.enabled = false;
            return;
        }
    }

    private void ManageRecovery()
    {
        float i = UpdateTimerState(recoveryTime);

        if (i >= 1) //End
        {
            isAttaking = false;
            pillowPivot.SetActive(false);
        }
    }


    bool duringFreezeFrame = false;

    private void OnHit()
    {
        freezeFrameTimer = 0;
        duringFreezeFrame = true;
    }
}
