using System.Collections.Generic;
using UnityEngine;

namespace Client.Scripts
{
    public class CreateCubesState: IState
    {
        private const float SPAWN_RANGE = 14f;
        
        private const int MIN_CUBES_COUNT = 15;
        private const int MAX_CUBES_COUNT = 25;

        private readonly Cube _cubePrefab;

        private Cube[] _cubes;
        public Cube[] GetCubes => _cubes;

        public CreateCubesState()
        {
            _cubePrefab = Resources.Load<Cube>("Prefabs/Cube");
        }
        
        public void EnterState()
        {
            CreateCubes();
        }
        
        public void ExitState() { }

        private void CreateCubes()
        {
            int randomCubeCount = Random.Range(MIN_CUBES_COUNT, MAX_CUBES_COUNT);
            _cubes = new Cube[randomCubeCount];
            for (int i = 0; i < randomCubeCount; i++)
            {
                Vector3 randomPosition = GetRandomSpawnPosition();
                Cube newCube = Object.Instantiate(_cubePrefab, randomPosition, Quaternion.identity);
                newCube.Initialize(i);
                
                _cubes[i] = newCube;
            }
        }

        private Vector3 GetRandomSpawnPosition()
        {
            float randomX = Random.Range(-SPAWN_RANGE, SPAWN_RANGE);
            float randomZ = Random.Range(-SPAWN_RANGE, SPAWN_RANGE);

            Vector3 randomVector = new Vector3(randomX, 10, randomZ);
            return randomVector;
        }
    }
}