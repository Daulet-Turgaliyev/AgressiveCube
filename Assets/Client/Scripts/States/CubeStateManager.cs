namespace Client.Scripts
{
    public class CubeStateManager
    {
        private IState _currentState;

        public void SetState(IState newState)
        {
            _currentState?.ExitState();

            _currentState = newState;
            _currentState.EnterState();
        }
    }
}