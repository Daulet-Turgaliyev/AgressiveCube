namespace Client.Scripts
{
    public class AttackCubesState: IState
    {
        private readonly Player _player;
        private readonly CreateCubesState _createCubes;


        public AttackCubesState(CreateCubesState createCubes, Player player)
        {
            _createCubes = createCubes;
            _player = player;
        }
        
        public void EnterState()
        {
            _player.StartAttack();
        }

        public void ExitState()
        {
            _player.StopAttack();
            
            var allActiveCubes = _createCubes.GetCubes;
            foreach (var cube in allActiveCubes)
            {
                cube.StartMovement();
            }
        }
    }
}