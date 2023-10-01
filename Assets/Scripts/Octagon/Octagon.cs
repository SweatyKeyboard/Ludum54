using System;
using System.Collections.Generic;
using Objects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Octagon
{
    public sealed class Octagon : MonoBehaviour
    {
        [SerializeField] private RecipePanel[] _recipePanels;
        [SerializeField] private ManaSOType[] _usualMana;
        [SerializeField] private ManaSOType[] _specialMana;
        [SerializeField] private BonusSOType[] _bonuses;
        [SerializeField] private Recipe[] _presetConvert;
        [SerializeField] private float _spawnDistance;

        private RecipeGenerator _generator;
        public event Action InventoryUpgraded; 
        public event Action PlayerSpeedUpgraded; 
        public event Action VoidKnockbacked;
        public event Action<ManaCollectable> GotMana;

        private int _currentLevel = 2;
        public void Init()
        {
            _generator = new RecipeGenerator(_usualMana, _specialMana, _bonuses);
            RegenerateRecipes();
            
            foreach (var panel in _recipePanels)
            {
                panel.Finished += recipe =>
                {
                    SpawnBonus(recipe);
                    RegenerateRecipes();
                };
            }
        }

        private void RegenerateRecipes()
        {
            if (_presetConvert.Length == 0)
            {
                var ignorable = new List<BonusSOType>(_recipePanels.Length - 1);
                foreach (var panel in _recipePanels)
                {
                    var recipe = _generator.Generate(_currentLevel, ignorable);
                    panel.GenerateRecipe(recipe);
                    ignorable.Add(recipe.Result);
                }
            }
            else
            {
                for (var i = 0; i < _recipePanels.Length; i++)
                {
                    _recipePanels[i].GenerateRecipe(_presetConvert[i]);
                }
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out a_Collectable collectable))
                return;

            foreach (var panel in _recipePanels)
            {
                panel.Put(((ManaCollectable)collectable).Type);
                Destroy(collectable.gameObject);
                GotMana?.Invoke((ManaCollectable)collectable);
            }
        }

        private void SpawnBonus(BonusSOType bonus)
        {
            _currentLevel++;
            
            var randomAngle = Random.Range(0, 360);
            var randomizePosition = new Vector2(
                    Mathf.Cos(randomAngle * Mathf.Deg2Rad),
                    Mathf.Sin(randomAngle * Mathf.Deg2Rad)) * _spawnDistance;
            var spawnedBonus = Instantiate(
                    bonus.Reward,
                    (Vector2)transform.position + randomizePosition,
                    Quaternion.identity);

            if (spawnedBonus is ManaCollectable collectable)
            {
                collectable.Init(((ManaBonusSOType)bonus).ManaType, Vector2.zero);
            }

            if (spawnedBonus is BonusCollectable bonusObj)
            {
                var data = ((BonusBonusSOType)bonus).BonusType;
                bonusObj.Init(data);
                bonusObj.Collected += () => GetBonusEffect(data.Data);
            }
        }

        private void GetBonusEffect(EBonusData bonusTypeData)
        {
            Action action;
            action = bonusTypeData switch
            {
                    EBonusData.InventoryUp => InventoryUpgraded,
                    EBonusData.SpeedUp => PlayerSpeedUpgraded,
                    EBonusData.StopTheVoid => VoidKnockbacked,
            };
            action?.Invoke();
        }
    }
}