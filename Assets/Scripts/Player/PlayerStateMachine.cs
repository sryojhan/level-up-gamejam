public class PlayerStateMachine
{
    public enum State
    {
        Iddle, Run, Attack,
        
        None
    }

    private State currentState = State.None;
    private State previousSate = State.None;

    public State state
    {
        get
        {
            return currentState;
        }
        set
        {
            previousSate = currentState;
            currentState = value;
        }
    }


    public bool HasChanged() => currentState != previousSate;

}
