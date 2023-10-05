using System.Collections;
using Tools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Client.Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] 
        private LayerMask _targetMask;
        [SerializeField]
        private Projectile _spherePrefab;
        [SerializeField] 
        private float _sizeVision = 15f;
        [SerializeField]
        private Transform _shootingPoint;
        [SerializeField] 
        private PopUpText _popUpText;
        
        private Transform _targetTransform;
        private Coroutine _shootCoroutine;
        
        private float _rotationSpeed = 25f;
        private readonly float _shootForce = 40f;
        
        private float _updateTargetCooldown = 50f;
        private float _cooldown = 1f;

        private bool _stopShoot;

        private int _targetIdentifier;

        private bool _isAttack;
        
        
        public void StartAttack()
        {
            _isAttack = true;
            StartCoroutine(StartFindAndAttackCube());
        }

        public void StopAttack()
        {
            _isAttack = false;
        }

        private IEnumerator StartFindAndAttackCube()
        {
            while (_isAttack == true)
            {
                if (TryUpdateNewTarget() == false)
                {
                    yield return Yielders.WaitForSeconds(5);
                }
                
                StartCoroutine(UpdatePosition());
                
                StartAttackCube();
                
                yield return Yielders.WaitForSeconds(_updateTargetCooldown);
            }
        }

        private void UpdateTarget()
        {
            TryUpdateNewTarget();
        }
        private bool TryUpdateNewTarget()
        {
            var results = new Collider[10];
            var count = Physics.OverlapBoxNonAlloc(Vector3.zero, Vector3.one * _sizeVision, results , Quaternion.identity, _targetMask);
            
            if (count == 0)
            {
                Debug.LogWarning("Can't find cubes");
                return false;
            }
            
            int randomIndex = Random.Range(0, count);
            Transform target = results[randomIndex].transform;
            _targetTransform = target;
            return true;
        }

        private IEnumerator UpdatePosition()
        {
            while (_isAttack == true)
            {
                if (_targetTransform == null)
                {
                    yield return Yielders.WaitForSeconds(1);
                }
                Vector3 targetDirection = _targetTransform.position - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

                yield return Yielders.EndOfFrame;
            }
        }

        private void StartAttackCube()
        {
            if (_shootCoroutine != null)
                StopCoroutine(_shootCoroutine);
            
            _shootCoroutine = StartCoroutine(ShootSphere());
        }
        
        private IEnumerator ShootSphere()
        {
            while (_isAttack == true)
            {
                if (_targetTransform == null)
                {
                    yield break;
                }

                if (_stopShoot == true)
                {
                    yield return Yielders.WaitForSeconds(.5f);
                }
                
                CreatePopup();
                
                int targetId = _targetTransform.GetComponent<Cube>().GetIdentifier;
                
                Projectile sphere = Instantiate(_spherePrefab, _shootingPoint.position, Quaternion.identity);
                Rigidbody sphereRigidbody = sphere.GetComponent<Rigidbody>();
                sphereRigidbody.AddForce(_shootingPoint.forward * _shootForce, ForceMode.Impulse);
                sphere.Initialize(targetId);
                
                sphere.onHit += UpdateTarget;
                
                yield return Yielders.WaitForSeconds(_cooldown);
            }
        }

        private void CreatePopup()
        {
            if (_targetTransform == null) return;
            int targetId = _targetTransform.GetComponent<Cube>().GetIdentifier;
            
            var popUp = Instantiate(_popUpText, transform.position, Quaternion.identity);
            popUp.Initialize(targetId);
        }
    }
}
