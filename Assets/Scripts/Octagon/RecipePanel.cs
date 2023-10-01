using System;
using Objects;
using UnityEngine;
using UnityEngine.UI;

namespace Octagon
{
    public sealed class RecipePanel : MonoBehaviour
    {
        [SerializeField] private RecipeManaPiece[] _pieces;
        [SerializeField] private Image _rewardIcon;

        private Recipe _recipe;
        public event Action<BonusSOType> Finished;
        public void GenerateRecipe(Recipe recipe)
        {
            _recipe = recipe;
            for (var i = 0; i < _pieces.Length; i++)
            {
                var isActive = i < recipe.ManaPieces.Count;
                _pieces[i].gameObject.SetActive(isActive);
                _pieces[i].Init(false, isActive ? recipe.ManaPieces[i] : null);
            }

            if (recipe.Result != null)
                _rewardIcon.sprite = recipe.Result.Sprite;
        }

        public void Put(ManaSOType type)
        {
            var result = false;
            foreach (var piece in _pieces)
            {
                if (piece.IsActive)
                    continue;
                if (piece.ManaType != type)
                    break;
                
                piece.Init(true, piece.ManaType);
                result = true;
                break;
            }

            if (!result)
            {
                ClearProgress();
            }
            
            CheckForFinished();
        }

        private void CheckForFinished()
        {
            var result = true;
            foreach (var piece in _pieces)
            {
                if (!piece.gameObject.activeSelf || piece.IsActive)
                    continue;
                
                result = false;
                break;
            }
            
            if (result)
                Finished?.Invoke(_recipe.Result);
        }

        private void ClearProgress()
        {
            foreach (var piece in _pieces)
            {
                piece.Init(false, piece.ManaType);
            }
        }
    }
}