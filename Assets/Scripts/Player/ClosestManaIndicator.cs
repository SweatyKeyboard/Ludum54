using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Objects;
using UnityEngine;

namespace Player
{
    public sealed class ClosestManaIndicator : MonoBehaviour
    {
        [SerializeField] private float _distaneFromPlayer;
        [field: SerializeField] public ManaSOType TargetType { get; private set; }
        
        private Transform _closestObject;
        private Transform _playerTransform;

        private bool _isReady;

        private List<ManaCollectable> _objectsList = new();

        public async UniTaskVoid Init(Transform playerTransform = null, ManaSOType objectType = null)
        {
            await UniTask.DelayFrame(1);
            
            if (playerTransform != null)
                _playerTransform ??= playerTransform;

            if (objectType != null)
            {
                _objectsList =  FindObjectsOfType<ManaCollectable>().Where(m => m.Type == objectType).ToList();
                _closestObject = GetClosestObject();
            }

            _isReady = _closestObject != null && _playerTransform != null;
            gameObject.SetActive(_isReady);
        }

        public void RemoveFromList(ManaCollectable obj)
        {
            _objectsList.Remove(obj);
        }

        private Transform GetClosestObject()
        {
            var hasElements = _objectsList.Count > 0;
            _isReady = hasElements;
            return !hasElements ? null : _objectsList.OrderBy(x => (x.transform.position - _playerTransform.position).magnitude).First().transform;
        }
        
        private void Update()
        {
            _closestObject = GetClosestObject();
            
            if (!_isReady)
                return;
            if (_closestObject == null)
                return;
            
            var angle = Mathf.Atan2(
                    _closestObject.position.y - _playerTransform.position.y,
                    _closestObject.position.x - _playerTransform.position.x) * Mathf.Rad2Deg - 90;
            transform.position = (Vector2)_playerTransform.position + ((Vector2)_closestObject.position - (Vector2)_playerTransform.position).normalized * _distaneFromPlayer;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}