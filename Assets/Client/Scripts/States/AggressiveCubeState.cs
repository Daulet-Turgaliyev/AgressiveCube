using UnityEngine;

namespace Client.Scripts
{
    public class AggressiveCubeState: IState
    {
        private readonly CreateCubesState _createCubes;

        public AggressiveCubeState(CreateCubesState createCubes)
        {
            _createCubes = createCubes;
        }
        
        public void EnterState()
        {
            var allActiveCubes = _createCubes.GetCubes;
            int randomCubeIndex = Random.Range(0, allActiveCubes.Length);
            allActiveCubes[randomCubeIndex].SetMegaCube();
        }

        public void ExitState()
        {
        }
    }
}