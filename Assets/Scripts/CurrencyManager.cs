using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CurrencyManager : Singleton<CurrencyManager>
{
    // Start is called before the first frame update
    private int crystals = 1;
    private int enemiesKilledToGainCrystal = 10;
    private float currentEnemiesKilledProgress = 0f;
    private int CrystalsGainedByEnemiesKilled = 1;

    private bool HasStarted = false;

    public EventHandler<CrystalsChangedEventArgs> onCrystalsChanged;

    void CrystalValueChanged()
    {
        onCrystalsChanged?.Invoke(this, new CrystalsChangedEventArgs(crystals));
    }
    public bool hasCrystals(int amount)
    {
        return crystals >= amount;
    }
    private bool SpendCrystals(int amount)
    {
        if (hasCrystals(amount))
        {
            crystals -= amount;
            CrystalValueChanged();
            return true;
        }
        return false;
    }

    public void HandleEnemyKilled(object sender, EnemyKilledEventArgs args)
    {
        currentEnemiesKilledProgress += 1f / enemiesKilledToGainCrystal;
        if (currentEnemiesKilledProgress >= 1f)
        {
            GainCrystals(CrystalsGainedByEnemiesKilled);
            currentEnemiesKilledProgress = 0f;
        }
    }

    private bool GainCrystals(int amount)
    {
        if (amount <= 0)
        {
            return false;
        }
        crystals += amount;
        CrystalValueChanged();
        return true;
    }
    public void HandleTowerSelected(object sender, TowerSelectionArgs args)
    {
        int cost = args.TowerPrefab.GetComponent<Tower>().Cost;
        SpendCrystals(cost);
    }

    // Update is called once per frame
    void Update()
    {
        if (!HasStarted)
        {
            HasStarted = !HasStarted;
            CrystalValueChanged();
        }
    }
}
