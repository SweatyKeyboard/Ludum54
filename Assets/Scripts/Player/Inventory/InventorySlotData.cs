using Objects;
using UnityEngine;

namespace Player.Inventory
{
    public sealed class InventorySlotData
    {
        public bool IsLocked { get; set; }
        public Sprite Sprite { get; set; }
        public ManaSOType ManaType { get; set; }

        public InventorySlotData(Sprite sprite = null, bool isLocked = false, ManaSOType manaType = null)
        {
            Sprite = sprite;
            IsLocked = isLocked;
            ManaType = manaType;
        }
    }
}