using UnityEngine;

namespace Player
{
    public sealed class ThrowPosition : MonoBehaviour
    {
        [SerializeField] private float _distance;
        private Rigidbody2D _rigidbody;
        private Transform _playerTransform;

        public void Init(Transform playerTransform, Rigidbody2D rigidbody)
        {
            _playerTransform = playerTransform;
            _rigidbody = rigidbody;
        }

        private void Update()
        {
            transform.position = (Vector2)_playerTransform.position + _rigidbody.velocity.normalized * _distance;
        }
    }
}