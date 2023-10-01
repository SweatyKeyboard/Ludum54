using System;
using System.Linq;
using Objects;
using UnityEngine;

namespace Player.Inventory
{
    public sealed class Inventory : MonoBehaviour
    {
        [SerializeField] private InventorySlot[] _slots = new InventorySlot[5];
        [SerializeField] private Transform _arrow;
        private int UnlockedSlotsCount => _slots.Count(s => !s.IsLocked);
        private int _selectedSlot;
        public void Init()
        {
            foreach (var slot in _slots)
            {
                slot.Init(true);
            }
            _slots[0].Init(false);
        }
        
        public bool TryPut(a_Collectable collectable)
        {
            foreach (var slot in _slots)
            {
                if (slot.Sprite != null)
                    continue;
                if (slot.IsLocked)
                    return false;

                slot.Put(collectable);
                break;
            }
            return true;
        }


        private void Update()
        {
            for (var i = 0; i < 5; i++)
            {
                if (Input.GetKeyDown((KeyCode)(i+49)))
                {
                    if (!_slots[i].IsLocked)
                    {
                        SelectSlot(i);
                        break;
                    }
                }
            }

            if (Input.mouseScrollDelta.y > 0)
            {
                var index = _selectedSlot++;
                if (_selectedSlot >= UnlockedSlotsCount)
                {
                    _selectedSlot = 0;
                }
                SelectSlot(_selectedSlot);
            }
            
            if (Input.mouseScrollDelta.y < 0)
            {
                var index = _selectedSlot--;
                if (_selectedSlot < 0)
                {
                    _selectedSlot = UnlockedSlotsCount - 1;
                }
                SelectSlot(_selectedSlot);
            }
        }

        public void SelectSlot(int index)
        {
            _selectedSlot = index;
            _arrow.position = new Vector2(_slots[index].transform.position.x, _arrow.position.y);
        }

        public ManaSOType Pull()
        {
            if (_slots[_selectedSlot]?.Sprite != null)
            {
                return _slots[_selectedSlot].Pull();
            } 
            return null;
            
        }

        public void Upgrade()
        {
            foreach (var slot in _slots)
            {
                if (!slot.IsLocked)
                    continue;

                slot.Init(false);
                return;
            }
        }
    }
}