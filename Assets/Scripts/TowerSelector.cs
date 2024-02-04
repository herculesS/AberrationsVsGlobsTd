using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerSelector : MonoBehaviour
{

    public GameObject _selectionWindowUI;
    public EventHandler<TowerSelectionArgs> OnTowerSelected;
    // Start is called before the first frame update
    void Start()
    {
        OnTowerSelected += SpawnTowerManager.Instance.HandleTowerSelected;
        SpawnTowerManager.Instance.OnArenaTileClicked += HandleArenaTileClicked;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HandleArenaTileClicked(object sender, ArenaTileClickedArgs args)
    {
        Debug.Log("Arena tile clicked should open selection window");
        _selectionWindowUI.SetActive(true);
        towerSelected();
    }

    public void towerSelected()
    {
        OnTowerSelected?.Invoke(this, new TowerSelectionArgs());

    }
}
