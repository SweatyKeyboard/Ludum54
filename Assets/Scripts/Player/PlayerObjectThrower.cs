using System;
using Objects;
using UnityEngine;

namespace Player
{
    public sealed class PlayerObjectThrower : MonoBehaviour
    {
        [SerializeField] private ManaCollectable _manaPrefab;
        [SerializeField] private Transform _transformToSpawn;
        [SerializeField] private Transform _spawnPos;
        
        public event Func<ManaSOType> GetThrowingObject;
        public event Action<ManaSOType> Throwed;

        private Rigidbody2D _rigidbody;

        public void Init(Rigidbody2D rigidbody)
        {
            _rigidbody = rigidbody;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ThrowObject();
            }
        }

        private void ThrowObject()
        {
            var manaType = GetThrowingObject?.Invoke();
            if (manaType != null)
            {
                var spawned = Instantiate(_manaPrefab, _spawnPos.position, Quaternion.identity, _transformToSpawn);
                spawned.Init(manaType, _rigidbody.velocity * 2f);
                Throwed?.Invoke(manaType);
            }
        }
    }
}