using System.Collections.Generic;
using System.Linq;
using Objects;
using UnityEngine;
using UsefulScripts;

namespace Octagon
{
    public sealed class RecipeGenerator
    {
        private ManaSOType[] _usualMana;
        private ManaSOType[] _specialMana;
        private BonusSOType[] _bonuses;

        public RecipeGenerator(ManaSOType[] usualMana, ManaSOType[] specialMana, BonusSOType[] bonuses)
        {
            _usualMana = usualMana;
            _specialMana = specialMana;
            _bonuses = bonuses;
        }

        public Recipe Generate(int piecesCount, List<BonusSOType> ignorableBonuses = null)
        {
            var list = new List<ManaSOType>(piecesCount);
            for (var i = 0; i < piecesCount; i++)
            {
                var isHard = Random.Range(0, 10) < 3;
                if (isHard && i < piecesCount - 1)
                    i++;
                list.Add(isHard ? _specialMana.GetRandomElement() : _usualMana.GetRandomElement());
            }

            BonusSOType bonus;
            do
            {
                bonus = _bonuses.GetRandomElement();
            } while (ignorableBonuses != null && ignorableBonuses.Contains(bonus));

            return new Recipe(list, bonus);
        }
    }
}