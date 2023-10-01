using Objects;
using UnityEngine;
using UnityEngine.UI;

namespace Octagon
{
    public sealed class RecipeManaPiece : MonoBehaviour
    {
        [SerializeField] private Image _backImage;
        [SerializeField] private Image _frontImage;

        [SerializeField] private Color _inactiveColor;
        [SerializeField] private Color _activeColor;
        
        public bool IsActive { get; private set; }
        public ManaSOType ManaType { get; private set; }

        public void Init(bool isActive, ManaSOType manaType)
        {
            IsActive = isActive;
            ManaType = manaType;
            
            if (manaType == null)
                return;
            
            _backImage.color = isActive ? _activeColor : _inactiveColor;
            _frontImage.color = isActive ? manaType.ActiveColor : manaType.InactiveColor;
        }
    }
}