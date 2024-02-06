using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ArenaTileClickedArgs : EventArgs
{
    public List<TowerSO> Towers { get; set; }
    public ArenaTileClickedArgs(List<TowerSO> _towers)
    {
        Towers = _towers;
    }

}