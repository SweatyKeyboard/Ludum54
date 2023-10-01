using System;
using System.Linq;
using Objects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public sealed class Player : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Rigidbody2D _rigidbody;
        [Header("PlayerComponents")]
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerCollector _playerCollector;
        [SerializeField] private Inventory.Inventory _inventory;
        [SerializeField] private ClosestManaIndicator[] _indicators;
        [SerializeField] private PlayerObjectThrower _objectThrower;
        [SerializeField] private ThrowPosition _throwPosition;

        public event Action Dead;
        
        public void Init()
        {
            _playerMovement.Init(_rigidbody);
            _inventory.Init();
            _throwPosition.Init(transform, _rigidbody);
            _objectThrower.Init(_rigidbody);
            
            _playerCollector.TriedToCollectMana += OnTriedToCollectMana;
            foreach (var indicator in _indicators)
            {
                indicator.Init(playerTransform: transform).Forget();
            }
            _objectThrower.GetThrowingObject += _inventory.Pull;
            _objectThrower.Throwed += type => _indicators.First(i => i.TargetType == type).Init(objectType: type);
        }

        private bool OnTriedToCollectMana(a_Collectable collectable)
        {
            var result = _inventory.TryPut(collectable);
            if (result && collectable is ManaCollectable manaCollectable)
            {
                OnManaCountChanged(manaCollectable);
                var indicatorOfThis = _indicators.Where(i => i.TargetType == manaCollectable.Type).ToArray();
                if (indicatorOfThis.Length > 0)
                {
                    indicatorOfThis[0].RemoveFromList(manaCollectable);
                    indicatorOfThis[0].Init(objectType: indicatorOfThis[0].TargetType).Forget();
                }
            }

            return result;
        }

        public void OnManaCountChanged(ManaCollectable type)
        {
            foreach (var indicator in _indicators)
            {
                if (indicator.TargetType == type.Type)
                {
                    indicator.Init(objectType: type.Type).Forget();
                }
            }
        }

        public void UpgradeInventory()
        {
            _inventory.Upgrade();
        }

        public void UpgradeSpeed()
        {
            _playerMovement.UpgradeSpeed();
        }

        public void RemoveMana(ManaCollectable type)
        {
            _indicators.First(i => i.TargetType == type.Type).RemoveFromList(type);
        }

        public void Die()
        {
            Dead?.Invoke();
        }
    }
}