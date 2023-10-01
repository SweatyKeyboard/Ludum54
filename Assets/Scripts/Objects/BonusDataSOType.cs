using UnityEngine;

namespace Objects
{
    [CreateAssetMenu(menuName = "BonusData")]
    public sealed class BonusDataSOType : ScriptableObject
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public EBonusData Data { get; private set; }
    }
}