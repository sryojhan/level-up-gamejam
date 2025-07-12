using UnityEngine;

[AddComponentMenu("Player/Animation controller")]
public class PlayerAnimationController : MonoBehaviour
{
    public float maxHorizontalSpeedToFrontRun = 0.3f;

    public void UpdateAnimation(Animator animator, SpriteRenderer sprRenderer, PlayerStateMachine playerState, PlayerDirection direction, Vector2 movementDirection)
    {

        var state = playerState.state;


        (bool south, bool east) = direction.IsSouthAndEastBooleanRepresentation();


        if(south && Mathf.Abs(movementDirection.x) < maxHorizontalSpeedToFrontRun && playerState.state == PlayerStateMachine.State.Run)
        {
            animator.Play("Run_forwards");
            sprRenderer.flipX = false;
            return;
        }



        string dir = south ? "front" : "back";

        sprRenderer.flipX = state == PlayerStateMachine.State.Iddle ? east :( south ? east : !east);

        string animationName = "";

        switch (state)
        {
            case PlayerStateMachine.State.Iddle:

                animationName = "Idle";
                break;
            case PlayerStateMachine.State.Run:
                animationName = "Run";
                break;
            case PlayerStateMachine.State.Attack:
                break;
            case PlayerStateMachine.State.None:
                break;
            default:
                break;
        }



        animator.Play(CombineAnimationName(animationName, dir));

    }


    private string CombineAnimationName(string animatinoName, string direction)
    {
        return animatinoName + "_" + direction;
    }

}
