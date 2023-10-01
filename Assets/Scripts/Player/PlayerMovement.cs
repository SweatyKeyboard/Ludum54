using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class PlayerMovement : MonoBehaviour
    {
        [Header("Components")] 
        [SerializeField] private SpriteRenderer _eyes;

        [Header("Fields")]
        [SerializeField] private float _speed;

        private Vector2 _movementVector;
        private Rigidbody2D _rigidbody;

        public void Init(Rigidbody2D rigidbody)
        {
            _rigidbody = rigidbody;
        }
        private void Update()
        {
            _movementVector = new Vector2(
                    Input.GetAxis("Horizontal"),
                    Input.GetAxis("Vertical"));
            _movementVector = (_movementVector.magnitude > 1 ? _movementVector.normalized : _movementVector) * _speed;
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = _movementVector;
            _eyes.transform.localPosition = _movementVector.normalized;
        }

        public void UpgradeSpeed()
        {
            _speed += 1.5f;
        }
    }
}