using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class TowerSelector : MonoBehaviour
{
    public GameObject _selectionWindowUI;
    public GameObject _towerOptionUI;
    public GameObject _containerUI;
    public EventHandler<TowerSelectionArgs> OnTowerSelected;

    private List<TowerOptionUI> towerOptions = new List<TowerOptionUI>();

    private bool isWindowOpen = false;
    void Start()
    {
        OnTowerSelected += SpawnTowerManager.Instance.HandleTowerSelected;
        OnTowerSelected += CurrencyManager.Instance.HandleTowerSelected;
        SpawnTowerManager.Instance.OnArenaTileClicked += HandleArenaTileClicked;
    }

    public void HandleArenaTileClicked(object sender, ArenaTileClickedArgs args)
    {
        ShowSelectionWindow();
        List<TowerSO> listOfTowerSO = args.Towers;
        foreach (TowerSO towerSO in listOfTowerSO)
        {
            Tower tower = towerSO.towerPrefab.GetComponent<Tower>();
            GameObject obj = Instantiate(_towerOptionUI, _containerUI.transform.position, Quaternion.identity, _containerUI.transform);
            TowerOptionUI optionUI = new TowerOptionUI(obj, tower.Cost, towerSO.towerIcon);
            if (optionUI.BuyButton != null)
            {
                if (!CurrencyManager.Instance.hasCrystals(tower.Cost))
                {
                    optionUI.BuyButton.interactable = false;
                }

                optionUI.BuyButton.onClick.AddListener(delegate { towerSelected(towerSO.towerPrefab, tower.Cost); });
            }
            towerOptions.Add(optionUI);
        }
    }


    public void towerSelected(GameObject towerSelected, int towerCost)
    {
        if (CurrencyManager.Instance.hasCrystals(towerCost))
        {
            ClearSelectorContainer();
            HideSelectionWindow();

            OnTowerSelected?.Invoke(this, new TowerSelectionArgs(towerSelected));
        }


    }

    void Update()
    {
        if (isWindowOpen)
        {
            foreach (TowerOptionUI option in towerOptions)
            {
                if (!option.BuyButton.interactable && CurrencyManager.Instance.hasCrystals(option.Cost))
                {
                    option.BuyButton.interactable = true;
                }
            }
        }
    }
    private void HideSelectionWindow()
    {
        isWindowOpen = false;
        _selectionWindowUI.SetActive(false);
    }
    private void ShowSelectionWindow()
    {
        isWindowOpen = true;
        _selectionWindowUI.SetActive(true);
    }

    private void ClearSelectorContainer()
    {
        towerOptions.Clear();
        foreach (Transform childTransform in _containerUI.transform)
        {
            Destroy(childTransform.gameObject);
        }
    }
}
