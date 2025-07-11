using UnityEngine;

[AddComponentMenu("Player/Animation controller")]
public class PlayerAnimationController : MonoBehaviour
{
    public void UpdateAnimation(Animator animator, SpriteRenderer sprRenderer, PlayerStateMachine playerState, PlayerDirection direction)
    {
        if (!playerState.HasChanged() && !direction.HasChanged()) return;

        var state = playerState.state;


        (bool south, bool east) = direction.IsSouthAndEastBooleanRepresentation();

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
