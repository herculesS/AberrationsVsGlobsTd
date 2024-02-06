using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TopBarBehavior : MonoBehaviour
{
    [SerializeField] private GameObject topBar;
    TMP_Text crystalsText;
    void Start()
    {
        GameObject crystalsView = topBar.transform.GetChild(0).GetChild(0).gameObject;
        crystalsText = crystalsView.GetComponent<TMP_Text>();
        CurrencyManager.Instance.onCrystalsChanged += HandleCrystallsChanged;
    }
    void HandleCrystallsChanged(object sender, CrystalsChangedEventArgs args)
    {
        crystalsText.text = args.newCrystalValue + "C";
    }
}
