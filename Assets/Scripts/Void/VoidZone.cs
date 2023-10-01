using System;
using Objects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Void
{
    public sealed class VoidZone : MonoBehaviour
    {
        [SerializeField] private float _sizeShrinkSpeed;
        [SerializeField] private TMP_Text _progressText;
        [SerializeField] private Image _fillImage;
        
        private float _beginningTimeToEnd;
        private float _timeToEnd;

        public event Action<ManaCollectable> ManaOnFieldChanged;

        private void Start()
        {
            _beginningTimeToEnd = transform.localScale.x / _sizeShrinkSpeed / 50;
        }

        private void FixedUpdate()
        {
            transform.localScale -= new Vector3(_sizeShrinkSpeed, _sizeShrinkSpeed, _sizeShrinkSpeed);
            _timeToEnd = transform.localScale.x / _sizeShrinkSpeed / 50;

            var progress = (_beginningTimeToEnd - _timeToEnd) / _beginningTimeToEnd;
            _progressText.text = (int)(progress * 100) + "%";
            _fillImage.fillAmount = progress;
        }

        public void Shrink()
        {
            Debug.Log(transform.localScale);
            transform.localScale += new Vector3(_sizeShrinkSpeed, _sizeShrinkSpeed, _sizeShrinkSpeed) * 1000;
            Debug.Log(transform.localScale);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player.Player player))
            {
                player.Die();
                return;
            }

            if (other.TryGetComponent(out ManaCollectable collectable))
            {
                Destroy(collectable.gameObject);
                ManaOnFieldChanged?.Invoke(collectable);
            }
        }
    }
}