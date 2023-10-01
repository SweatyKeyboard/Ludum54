using Objects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Player.Inventory
{
    public sealed class InventorySlot : MonoBehaviour
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private GameObject _lock;
        [SerializeField] private Sprite _emptySprite;

        private InventorySlotData _data;
        public bool IsLocked => _data.IsLocked;
        public Sprite Sprite => _data.Sprite;
        public ManaSOType ManaType => _data.ManaType;

        public void Init(bool isLocked)
        {
            _data = new InventorySlotData(isLocked: isLocked);
            UpdateView();
        }

        public void Put(a_Collectable collectable)
        {
            _data.Sprite = collectable.Sprite;
            _data.ManaType = ((ManaCollectable)collectable).Type;
            UpdateView();
        }
        
        public ManaSOType Pull()
        {
            var manaType = _data.ManaType;
            _data.Sprite = null;
            _data.ManaType = null;
            UpdateView();
            return manaType;
        }

        private void UpdateView()
        {
            _lock.SetActive(IsLocked);
            _itemImage.sprite = _data.Sprite == null ? _emptySprite : _data.Sprite;
        }
    }
}