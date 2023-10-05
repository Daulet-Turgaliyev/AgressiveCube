namespace Client.Scripts
{
    public class MoveCubesState: IState
    {
        private readonly CreateCubesState _createCubes;

        public MoveCubesState(CreateCubesState createCubes)
        {
            _createCubes = createCubes;
        }
        
        public void EnterState()
        {
            var allActiveCubes = _createCubes.GetCubes;
            foreach (var cube in allActiveCubes)
            {
                cube.StartMovement();
            }
        }

        public void ExitState()
        {

        }
    }
}