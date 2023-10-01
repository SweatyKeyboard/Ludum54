using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefulScripts;
using Random = UnityEngine.Random;

namespace Objects
{
    public sealed class ManaGenerator : MonoBehaviour
    {
        [SerializeField] private float _generatingOuterRadius;
        [SerializeField] private float _generatingInnerRadius;
        [SerializeField] private ManaSOType[] _SODataCollection;
        [SerializeField] private ManaCollectable _manaPrefab;
        [SerializeField] private Transform _manaSpwnTransform;
        private List<ManaCollectable> ManaList { get; } = new();
        private WaitForSeconds _wait;

        public event Action<ManaCollectable> Spawned;
        public event Func<float> GetVoidScale;
        public void Init()
        {
            StartCoroutine(SpawnMana());
        }

        private void CreateMana()
        {
            var mana = Instantiate(_manaPrefab, GeneratePosition(), Quaternion.identity, _manaSpwnTransform);
            mana.Init(_SODataCollection.GetRandomElement(), Vector2.zero);
            ManaList.Add(mana);
            mana.Collected += () => ManaList.Remove(mana);
            Spawned?.Invoke(mana);
        }

        private Vector2 GeneratePosition()
        {
            _generatingOuterRadius = GetVoidScale() * 10f - 2f;
            if (_generatingOuterRadius < _generatingInnerRadius)
            {
                StopCoroutine(SpawnMana());
            }
            
            var angle = Random.Range(0, 360);
            var distance = Random.Range(_generatingInnerRadius, _generatingOuterRadius);
            return new Vector2(
                    Mathf.Cos(angle * Mathf.Deg2Rad),
                    Mathf.Sin(angle * Mathf.Deg2Rad)) * distance;
        }


        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _generatingInnerRadius);
            Gizmos.DrawWireSphere(transform.position, _generatingOuterRadius);
        }


        private IEnumerator SpawnMana()
        {
            _wait = new WaitForSeconds(4f);
            
            while (true)
            {
                CreateMana();
                yield return _wait;
            }
        }
    }
}