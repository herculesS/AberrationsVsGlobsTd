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
    void Start()
    {
        OnTowerSelected += SpawnTowerManager.Instance.HandleTowerSelected;
        SpawnTowerManager.Instance.OnArenaTileClicked += HandleArenaTileClicked;
    }

    public void HandleArenaTileClicked(object sender, ArenaTileClickedArgs args)
    {
        ShowSelectionWindow();
        List<TowerSO> listOfTowerSO = args.Towers;
        foreach (TowerSO towerSO in listOfTowerSO)
        {
            Tower tower = towerSO.towerPrefab.GetComponent<Tower>();
            SetupNewTowerOptionUI(tower.Cost + "c", towerSO.towerIcon, towerSO.towerPrefab);
        }
    }

    private void SetupNewTowerOptionUI(string cost, Sprite icon, GameObject towerPrefab)
    {
        GameObject obj = Instantiate(_towerOptionUI, _containerUI.transform.position, Quaternion.identity, _containerUI.transform);

        TMP_Text costText = obj.transform.GetChild(0).GetComponent<TMP_Text>();
        Image towerIcon = obj.transform.GetChild(1).GetComponent<Image>();
        Button btn = obj.GetComponent<Button>();
        if (costText != null)
        {
            costText.text = cost;
        }
        if (towerIcon != null)
        {
            towerIcon.sprite = icon;
        }
        if (btn != null)
        {
            btn.onClick.AddListener(delegate { towerSelected(towerPrefab); });
        }
    }

    public void towerSelected(GameObject towerSelected)
    {
        ClearSelectorContainer();
        HideSelectionWindow();

        OnTowerSelected?.Invoke(this, new TowerSelectionArgs(towerSelected));

    }

    private void HideSelectionWindow()
    {
        _selectionWindowUI.SetActive(false);
    }
    private void ShowSelectionWindow()
    {
        _selectionWindowUI.SetActive(true);
    }

    private void ClearSelectorContainer()
    {
        foreach (Transform childTransform in _containerUI.transform)
        {
            Destroy(childTransform.gameObject);
        }
    }
}
