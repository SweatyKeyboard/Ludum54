using Octagon;
using UnityEngine;

namespace Objects
{
    public sealed class BonusCollectable : a_Collectable
    {
        [SerializeField] private BonusDataSOType _bonus;
        public override Sprite Sprite => _bonus.Sprite;

        public void Init(BonusDataSOType bonus)
        {
            _bonus = bonus;
            _spriteRenderer.sprite = Sprite;
        }
    }
}