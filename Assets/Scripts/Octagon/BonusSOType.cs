using Objects;
using UnityEngine;

namespace Octagon
{
    public abstract class BonusSOType : ScriptableObject
    {
        [field: SerializeField] public a_Collectable Reward { get; private set; }
        public abstract Sprite Sprite { get; }
    }
}