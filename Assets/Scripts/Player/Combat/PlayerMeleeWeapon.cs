using UnityEngine;

public class PlayerMeleeWeapon : MonoBehaviour
{
    public GameObject pillowPivot;

    private bool isAttaking = false;
    public float attackDuration = 1;

    private float attackTime = 0;
    private bool lastAttackDirection = false;

    public Quaternion minRotation;
    public Quaternion maxRotation;

    public Interpolation interpolation;

    private bool beginAttackThisFrame = false;

    public Vector2 handHorizontalPosition;
    public Vector2 handVerticalPosition;

    private void Start()
    {
        pillowPivot.SetActive(false);
    }

    public void BeginAttack(bool attackButtonPressed, PlayerDirection direction)
    {
        if (isAttaking) return;

        if (attackButtonPressed)
        {
            isAttaking = true;
            beginAttackThisFrame = true;

            Vector2 attackDirection = direction.GetVectorDirection();

            pillowPivot.transform.parent.localRotation = Quaternion.Euler(0, 0, Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg);

            print(direction.GetVectorDirection().ToString() + " " + Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg);

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

        }
    }

    public void UpdateAttack()
    {
        if (!isAttaking) return;

        if (beginAttackThisFrame) //Begin attack
        {
            attackTime = Time.time;
            lastAttackDirection = !lastAttackDirection;
            beginAttackThisFrame = false;


            pillowPivot.SetActive(true);
        }

        float i = (Time.time - attackTime) / attackDuration;


        if(i >= 1) //End attack
        {
            isAttaking = false;
            pillowPivot.SetActive(false);
            return;
        }


        Quaternion A = lastAttackDirection ? maxRotation : minRotation;
        Quaternion B = lastAttackDirection ? minRotation : maxRotation;


        pillowPivot.transform.localRotation = Quaternion.LerpUnclamped(A, B, interpolation.Interpolate(i));
    }
}
