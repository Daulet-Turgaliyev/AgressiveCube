using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

namespace Client.Scripts
{
    [RequireComponent(typeof(MeshRenderer), typeof(CubeRandomMovement))]
    public class Cube: MonoBehaviour
    {
        [SerializeField]
        private TextMeshPro[] _identifierTexts;
        
        private MeshRenderer _meshRenderer;
        private CubeRandomMovement _cubeRandomMovement;
        private CrazyCube _crazyCube;
        
        private int _identifier;
        public int GetIdentifier => _identifier;
        
        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _cubeRandomMovement = GetComponent<CubeRandomMovement>();
            _crazyCube = GetComponent<CrazyCube>();
        }

        public void Initialize(int identifier)
        {
            _identifier = identifier;
            
            foreach (var textMesh in _identifierTexts)
            {
                textMesh.text = identifier.ToString();
            }

            _meshRenderer.material.color = Random.ColorHSV();
        }

        public void StartMovement()
        {
            _cubeRandomMovement.StartMovement();
        }
        
        public void StopMovement()
        {
            _cubeRandomMovement.StopMovement();
        }

        public void SetMegaCube()
        {
            StopMovement();
            _crazyCube.StartCrazyMove();
        }
        
        
    }
}