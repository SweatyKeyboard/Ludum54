using Objects;
using UnityEngine;

namespace Octagon
{
    [CreateAssetMenu(menuName = "ManaBonus")]
    public sealed class ManaBonusSOType : BonusSOType
    {
        [field: SerializeField] public ManaSOType ManaType { get; private set; }
        public override Sprite Sprite => ManaType.Sprite;
    }
}