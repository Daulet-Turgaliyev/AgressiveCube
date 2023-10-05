using System.Collections;
using Tools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Client.Scripts
{
    public class CubeRandomMovement : MonoBehaviour
    {
        private readonly float _force = 5f;
        private readonly float _randomDirection = 1f;

        private Rigidbody _rigidbody;
        private Vector3 _targetVelocity;

        private bool _canMovement;
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void StartMovement()
        {
            _rigidbody.isKinematic = false;
            _canMovement = true;
            StartCoroutine(FixedMovementUpdate());
            StartCoroutine(RandomizeDirection());
        }

        public void StopMovement()
        {
            _rigidbody.isKinematic = true;
            _canMovement = false;
        }
        
        private IEnumerator RandomizeDirection()
        {
            while (_canMovement == true)
            {
                Vector3 randomDirection = GetRandomDirection().normalized;
                _targetVelocity = randomDirection * _force;
                yield return Yielders.WaitForSeconds(Random.Range(1f, 3f));
            }
        }

        private IEnumerator FixedMovementUpdate()
        {
            while (_canMovement == true)
            {
                _rigidbody.velocity = _targetVelocity;
                yield return Yielders.FixedUpdate;
            }
        }
        
        private Vector3 GetRandomDirection()
        {
            float randomX = Random.Range(-_randomDirection, _randomDirection);
            float randomZ = Random.Range(-_randomDirection, _randomDirection);

            Vector3 randomVector = new Vector3(randomX, 0, randomZ);
            return randomVector;
        }
    }
}
