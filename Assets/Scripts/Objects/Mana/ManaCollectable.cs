using UnityEngine;
namespace Objects
{
    public sealed class ManaCollectable : a_Collectable
    {
        [Header("Fields")]
        [SerializeField] private ManaSOType _type;
        [SerializeField] private Rigidbody2D _rigidbody;
        public void Init(ManaSOType type, Vector2 startVelocity)
        {
            _type = type;
            _spriteRenderer.sprite = Sprite;
            _rigidbody.velocity = startVelocity;
        }

        public override Sprite Sprite => _type.Sprite;
        public ManaSOType Type => _type;
    }
}