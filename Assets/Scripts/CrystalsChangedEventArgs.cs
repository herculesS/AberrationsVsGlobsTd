using UnityEngine;
using System;

public class CrystalsChangedEventArgs : EventArgs
{
    public int newCrystalValue;

    public CrystalsChangedEventArgs(int amount)
    {
        newCrystalValue = amount;
    }
}