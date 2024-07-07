namespace RadgarGames.Interface
{
    public interface IStateMachine
    {
        void ChangeState(IState newState);
    }
}