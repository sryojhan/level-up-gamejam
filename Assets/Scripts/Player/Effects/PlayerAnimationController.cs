using UnityEngine;

[AddComponentMenu("Player/Animation controller")]
public class PlayerAnimationController : MonoBehaviour
{
    public void UpdateAnimation(Animator animator, PlayerStateMachine playerState, PlayerDirection direction)
    {
        if (!playerState.HasChanged() && !direction.HasChanged()) return;

        var state = playerState.state;

        string dir = direction.GetDirectionString();

        string animationName = "";

        switch (state)
        {
            case PlayerStateMachine.State.Iddle:

                animationName = "Idle";
                break;
            case PlayerStateMachine.State.Run:
                animationName = "Run";
                break;
            case PlayerStateMachine.State.Roll:
                break;
            case PlayerStateMachine.State.Stun:
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
