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
        float shopBarHeight = shopBar.GetComponent<RectTransform>().rect.height / 1.2f;

        RectTransform battRectTransform = battery.GetComponent<RectTransform>();
        if (!shopBar.activeSelf)
        {
            battRectTransform.position = new Vector3(battRectTransform.position.x, battRectTransform.position.y + shopBarHeight);
            shopBar.SetActive(true);
            AudioManager.Instance.StopChillMusic();
            AudioManager.Instance.PlayBuildingMusic();
        }
        else
        {
            battRectTransform.position = new Vector3(battRectTransform.position.x, battRectTransform.position.y - shopBarHeight);
            shopBar.SetActive(false);
            AudioManager.Instance.StopBuildingMusic();
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
        mainMenuUI.SetActive(false);
        gameUI.SetActive(true);
        clickSound.Play();
        AudioManager.Instance.mainMenuMusic.Stop();
        AudioManager.Instance.PlayChillMusic();

    }

    public void Quit()
    {
        clickSound.Play();
        Application.Quit();
        AudioManager.Instance.mainMenuMusic.Stop();
    }

    public void Menu()
    {
        mainMenuUI.SetActive(true);
        clickSound.Play();
    }
}
