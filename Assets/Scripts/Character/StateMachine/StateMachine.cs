public class StateMachine
{
    public StateMachineFactory machineFactory;
    public State CurrState { get; private set; }

    public StateMachine(StateMachineFactory machineFactory)
    {
        this.machineFactory = machineFactory;
    }

    public void Initialize(State startingState)
    {
        CurrState = startingState;
        CurrState.Enter();
    }

    public void ChangeState(State newState, bool forceReset = false)
    {
        if(CurrState != newState || forceReset)
        {
            CurrState?.Exit();

            CurrState = newState;
            CurrState.Enter();

            machineFactory.SetCurrState(newState.ToString());
        }
    }
}
