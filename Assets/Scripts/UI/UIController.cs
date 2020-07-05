using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    #region Singleton

    private static UIController _instance;

    public static UIController Instance => _instance;

    #endregion

    public GameObject gameUI, mainMenuUI;
    public GameObject shopBar;
    public GameObject battery;
    public TextMeshProUGUI balanceText, carsChargingText;
    public AudioSource clickSound;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameUI.activeSelf)
        {
            Time.timeScale = 1.0f;
            UpdateUI();
        }
        else
        {
            Time.timeScale = 0.0f;
        }
            
    }

    public void ToggleShopBar()
    {
        float shopBarHeight = shopBar.GetComponent<RectTransform>().rect.height / 1.5f;

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

    private void UpdateUI()
    {
        float energyInput = PowerSystemManager.Instance.CurrentEnergyInput;
        float energyOutput = PowerSystemManager.Instance.CurrentEnergyOutput;
        float energyConverted = PowerSystemManager.Instance.StoredEnergy / PowerSystemManager.Instance.EnergyCapacity;
        if (energyConverted > 0.0f)
            BatteryController.Instance.SetCharge((int)(energyConverted * 10));

        BatteryController.Instance.SetInputAndOutput(energyInput, energyOutput);
        balanceText.SetText($"{BuildManager.Instance.Balance:F2}");
        carsChargingText.SetText($"{ChargingPadManager.Instance.SlotsInUse}");
    }

    public void Play()
    {
        Time.timeScale = 1.0f;
        mainMenuUI.SetActive(false);
        gameUI.SetActive(true);
        clickSound.Play();
    }

    public void Quit()
    {
        clickSound.Play();
        Application.Quit();
    }

    public void Menu()
    {
        Time.timeScale = 0.0f;
        mainMenuUI.SetActive(true);
        clickSound.Play();
    }
}
