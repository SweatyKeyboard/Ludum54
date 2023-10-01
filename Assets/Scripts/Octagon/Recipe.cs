using System;
using System.Collections.Generic;
using Objects;
using UnityEngine;

namespace Octagon
{
    [Serializable]
    public sealed class Recipe
    {
        [SerializeField] private List<ManaSOType> _manaPieces;
        [SerializeField] private BonusSOType _result;

        public List<ManaSOType> ManaPieces
        {
            get => _manaPieces;
            private set => _manaPieces = value;
        }
        public BonusSOType Result
        {
            get => _result;
            private set => _result = value;
        }

        public Recipe(List<ManaSOType> manaPieces, BonusSOType result)
        {
            ManaPieces = manaPieces;
            Result = result;
        }
    }
}