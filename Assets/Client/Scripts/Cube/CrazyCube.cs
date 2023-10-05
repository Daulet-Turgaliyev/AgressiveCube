using System.Collections;
using Tools;
using UnityEngine;

namespace Client.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class CrazyCube: MonoBehaviour
    {
        [SerializeField] 
        private LayerMask _targetMask;
        
        private readonly float _force = 5;
        private readonly float _sizeVision = 45f;

        private Rigidbody _rigidbody;
        private Transform _target;

        private bool _canMovement;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void StartCrazyMove()
        {
            transform.localScale = transform.localScale * 2f;
            
            _canMovement = true;
            _rigidbody.isKinematic = false;
            
            FindTarget();
            StartCoroutine(FixedMovementUpdate());
        }
        
        
        private IEnumerator FixedMovementUpdate()
        {
            while (_canMovement == true)
            {
                if (_target != null)
                {
                    Vector3 direction = (_target.position - transform.position).normalized;
                    Vector3 velocity = direction * _force;

                    _rigidbody.velocity = velocity;
                }
                else
                {
                    FindTarget();
                    _rigidbody.velocity = Vector3.zero;
                    yield return Yielders.WaitForSeconds(1);
                }

                yield return Yielders.FixedUpdate;
            }
        }

        private void FindTarget()
        {
            var results = new Collider[10];
            var count = Physics.OverlapSphereNonAlloc(transform.position, _sizeVision, results, _targetMask);

            if (count == 0)
            {
                Debug.LogWarning("Can't find cubes");
                return;
            }

            Transform closestTransform = null;
            float closestDistance = Mathf.Infinity;

            for (int i = 0; i < count; i++)
            {
                float distance = Vector3.Distance(transform.position, results[i].transform.position);
                if (distance < closestDistance && results[i].transform != transform)
                {
                    closestDistance = distance;
                    closestTransform = results[i].transform;
                }

                _target = closestTransform;
            }


            if (_target == null)
            {
                Debug.LogWarning("Can't find cubes");
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(_canMovement == false) return;

            if (collision.collider.TryGetComponent(out Cube cube))
            {
                if(_target == null) return;
                
                int targetId = _target.GetComponent<Cube>().GetIdentifier;

                if (targetId == cube.GetIdentifier)
                {
                    Destroy(cube.gameObject);
                }
            }
        }
    }
}