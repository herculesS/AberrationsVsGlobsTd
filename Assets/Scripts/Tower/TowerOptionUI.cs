using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerOptionUI
{
    GameObject optionPrefab;
    TMP_Text costText;
    Image towerIcon;

    int cost;

    Button buyButton;

    public TowerOptionUI(GameObject obj, int cost, Sprite icon)
    {
        OptionPrefab = obj;
        CostText = OptionPrefab.transform.GetChild(0).GetComponent<TMP_Text>();
        TowerIcon = OptionPrefab.transform.GetChild(1).GetComponent<Image>();
        buyButton = obj.GetComponent<Button>();
        this.Cost = cost;
        CostText.text = cost + "C";
        towerIcon.sprite = icon;
    }

    public GameObject OptionPrefab { get => optionPrefab; set => optionPrefab = value; }
    public TMP_Text CostText { get => costText; set => costText = value; }
    public Image TowerIcon { get => towerIcon; set => towerIcon = value; }
    public Button BuyButton { get => buyButton; set => buyButton = value; }
    public int Cost { get => cost; set => cost = value; }
}