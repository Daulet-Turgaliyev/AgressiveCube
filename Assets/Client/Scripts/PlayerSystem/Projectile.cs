using System;
using UnityEngine;

namespace Client.Scripts
{
    public class Projectile : MonoBehaviour
    {
        private int _id;
        public event Action onHit = () => { };

        public void Initialize(int id)
        {
            _id = id;
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent(out Cube cube))
            {
                if (_id == cube.GetIdentifier)
                {
                    cube.StopMovement();
                    onHit?.Invoke();
                }
            }

            Destroy(gameObject);
        }
    }
}
