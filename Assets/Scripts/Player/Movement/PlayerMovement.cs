using UnityEngine;

[AddComponentMenu("Player/Movement")]
public class PlayerMovement : MonoBehaviour
{
    public float maxMovementSpeed = 10;

    public float accelerationRate = 10;

    public float decelerationRate = 10;

    public float accelerationPower = 1;

    private Vector2 input;

    /* Called from Update */
    public void UpdatePlayerInput(Vector2 leftStickInput)
    {
        if (!isActiveAndEnabled)
        {
            input = Vector2.zero;
            return;
        }

        input = leftStickInput.normalized;
    }


    /* Called from Fixed Update */
    public void ApplyMovement(Rigidbody2D rigidBody)
    {
        if (!isActiveAndEnabled) return;

        Vector2 targetVelocity = input.normalized * maxMovementSpeed;

        Vector2 velocityDiff = targetVelocity - rigidBody.linearVelocity;

        float rateX = Mathf.Abs(targetVelocity.x) > Mathf.Abs(rigidBody.linearVelocity.x) ? accelerationRate : decelerationRate;
        float rateY = Mathf.Abs(targetVelocity.y) > Mathf.Abs(rigidBody.linearVelocity.y) ? accelerationRate : decelerationRate;


        float forceX = Mathf.Pow(Mathf.Abs(velocityDiff.x) * rateX, accelerationPower) * Mathf.Sign(velocityDiff.x);
        float forceY = Mathf.Pow(Mathf.Abs(velocityDiff.y) * rateY, accelerationPower) * Mathf.Sign(velocityDiff.y);

        Vector2 force = new Vector2(forceX, forceY);
        rigidBody.AddForce(force, ForceMode2D.Force);
    }


    public void UpdateMovementDirection(PlayerDirection playerDirection)
    {
        (bool south, bool east) = playerDirection.IsSouthAndEastBooleanRepresentation();

        if (input != Vector2.zero)
        {

            if (input.y > 0.01)
            {
                south = false;
            }
            else if (input.y < -0.01)
            {
                south = true;
            }

            else if (input.x > 0.01)
            {
                east = true;
            }
            else if (input.x < -0.01)
            {
                east = false;
            }


            playerDirection.UpdateWithSouthAndEastBooleanRepresentation(south, east);
        }
    }

}
