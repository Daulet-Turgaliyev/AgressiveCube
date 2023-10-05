namespace Client.Scripts
{
    public interface IState
    {
        void EnterState();
        void ExitState();
    }
}