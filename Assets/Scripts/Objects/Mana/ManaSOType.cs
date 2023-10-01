using UnityEngine;

namespace Objects
{
    [CreateAssetMenu(menuName = "ManaType")]
    public sealed class ManaSOType : ScriptableObject
    {
        [field: SerializeField] public Sprite Sprite { get; private set;}
        [field: SerializeField] public Color InactiveColor { get; private set; }
        [field: SerializeField] public Color ActiveColor { get; private set; }
    }
}