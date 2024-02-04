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
        _selectionWindowUI.SetActive(true);
        List<TowerSO> towers = args.Towers;
        for (int i = 0; i < towers.Count; i++)
        {
            Tower twr = towers[i].towerPrefab.GetComponent<Tower>();
            GameObject obj = Instantiate(_towerOptionUI, _containerUI.transform.position, Quaternion.identity, _containerUI.transform);
            TMP_Text costText = obj.transform.GetChild(0).GetComponent<TMP_Text>();
            costText.text = twr.Cost + "c";
            Image image = obj.transform.GetChild(1).GetComponent<Image>();
            image.sprite = towers[i].towerIcon;
            Button btn = obj.GetComponent<Button>();
            GameObject twrPrefab = towers[i].towerPrefab;
            btn.onClick.AddListener(delegate { towerSelected(twrPrefab); });
        }

    }

    public void towerSelected(GameObject towerSelected)
    {
        foreach (Transform item in _containerUI.transform)
        {
            Destroy(item.gameObject);
        }

        _selectionWindowUI.SetActive(false);

        OnTowerSelected?.Invoke(this, new TowerSelectionArgs(towerSelected));

    }
}
