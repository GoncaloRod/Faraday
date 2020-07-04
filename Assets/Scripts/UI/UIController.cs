using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject shopBar;
    public GameObject battery;
    public TextMeshProUGUI balanceText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float energyInput = PowerSystemManager.Instance.CurrentEnergyInput;
        float energyOutput = PowerSystemManager.Instance.CurrentEnergyOutput;
        float energyConverted = PowerSystemManager.Instance.StoredEnergy / PowerSystemManager.Instance.EnergyCapacity;
        if(energyConverted > 0.0f) 
            BatteryController.Instance.SetCharge((int) (energyConverted * 10));

        BatteryController.Instance.SetInputAndOutput(energyInput, energyOutput);
        UpdateBalanceText();
    }

    public void ToggleShopBar()
    {
        float shopBarHeight = shopBar.GetComponent<RectTransform>().rect.height / 1.65f ;

        RectTransform battRectTransform = battery.GetComponent<RectTransform>();
        if (!shopBar.activeSelf)
        {
            Debug.Log($"BattY: {battRectTransform.position.y} | ShopBarHeight: {shopBarHeight}");
            battRectTransform.position = new Vector3(battRectTransform.position.x, battRectTransform.position.y + shopBarHeight);
            Debug.Log($"BattY: {battRectTransform.position.y} | ShopBarHeight: {shopBarHeight}");
            shopBar.SetActive(true);
        }
        else
        {
            battRectTransform.position = new Vector3(battRectTransform.position.x, battRectTransform.position.y - shopBarHeight);
            shopBar.SetActive(false);
        }
    }

    private void UpdateBalanceText() => balanceText.SetText($"{BuildManager.Instance.Balance:F2}");
}
