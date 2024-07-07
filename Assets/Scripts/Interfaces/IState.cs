namespace RadgarGames.Interface
{
    public interface IState
    {
        void Enter();
        void Execute();
        void Exit();
    }
}