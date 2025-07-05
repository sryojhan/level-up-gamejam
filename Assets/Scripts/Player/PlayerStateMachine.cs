public class PlayerStateMachine
{
    public enum State
    {
        Iddle, Run, Roll, Stun, 
        
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
