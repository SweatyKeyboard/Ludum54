using Objects;
using UnityEngine;

namespace Octagon
{
    [CreateAssetMenu(menuName = "Bonus")]
    public sealed class BonusBonusSOType : BonusSOType
    {
        [field: SerializeField] public BonusDataSOType BonusType { get; private set; }
        public override Sprite Sprite => BonusType.Sprite;
    }
}