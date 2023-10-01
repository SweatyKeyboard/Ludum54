using System;
using Objects;
using UI;
using UnityEngine;
using Void;

public sealed class EnterPoint : MonoBehaviour
{
    [field: SerializeField] public Player.Player Player { get; private set; }
    [field: SerializeField] public ManaGenerator ManaGenerator { get; private set; }
    [field: SerializeField] public Octagon.Octagon[] BonusOctagons { get; private set; }
    [field: SerializeField] public Octagon.Octagon[] Octagons { get; private set; }
    [field: SerializeField] public VoidZone VoidZone { get; private set; }
    [field: SerializeField] public EndGameWindow EndGameWindow { get; private set; }

    private float _time;

    private void Init()
    {
        Time.timeScale = 1f;
        ManaGenerator.GetVoidScale += () => VoidZone.transform.localScale.x;
        ManaGenerator.Init();
        Player.Init();
        foreach (var octagon in Octagons)
        {
            octagon.Init();
            octagon.GotMana += Player.RemoveMana;
        }
        
        foreach (var octagon in BonusOctagons)
        {
            octagon.Init();
            octagon.InventoryUpgraded += Player.UpgradeInventory;
            octagon.PlayerSpeedUpgraded += Player.UpgradeSpeed;
            octagon.VoidKnockbacked += VoidZone.Shrink;
            octagon.GotMana += Player.RemoveMana;
        }
        
        ManaGenerator.Spawned += Player.OnManaCountChanged;
        VoidZone.ManaOnFieldChanged += Player.RemoveMana;
        Player.Dead += () => EndGameWindow.Show(_time);
    }

    private void Update()
    {
        _time += Time.deltaTime;
    }

    private void Awake()
    {
        Init();
    }
}