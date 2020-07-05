using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSystemManager : MonoBehaviour
{
    #region Singleton

    private static PowerSystemManager _instance;
    public static PowerSystemManager Instance => _instance;

    #endregion

    private float _timer;

    private float _storedEnergy = 0f;
    private float _energyCapacity = 0f;

    private float _currentEnergyInput = 0f;
    private float _currentEnergyOutput = 0f;

    private int _solarPanelCapacity = 0;

    private List<GameObject> _solarPanels = new List<GameObject>();
    private List<GameObject> _batteries = new List<GameObject>();
    private List<GameObject> _electricBoxes = new List<GameObject>();

    public float StoredEnergy => _storedEnergy;
    
    public float EnergyCapacity => _energyCapacity;

    public float CurrentEnergyInput { get; private set; } = 0f;

    public float CurrentEnergyOutput { get; private set; } = 0f;

    public int SolarPanelCount => _solarPanels.Count;

    public int SolarPanelCapacity => _solarPanelCapacity;

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

    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= 1f)
        {
            CurrentEnergyInput = _currentEnergyInput;
            CurrentEnergyOutput = _currentEnergyOutput;

            _currentEnergyInput = _currentEnergyOutput = 0;
            
            _timer = 0f;
        }

        float input = 0;

        foreach (GameObject solarPanel in _solarPanels)
        {
            input += solarPanel.GetComponent<SolarPanel>().CurrentOutput * Time.deltaTime;
        }

        _currentEnergyInput += input;

        if (_storedEnergy + input > _energyCapacity)
        {
            _storedEnergy = _energyCapacity;
        }
        else
        {
            _storedEnergy += input;
        }
    }

    public void AddSolarPanel(GameObject solarPanel)
    {
        if (solarPanel.GetComponent<SolarPanel>() == null)
            throw new Exception($"Mate {solarPanel.name} is not a SolarPanel!");

        if (_solarPanels.Count < _solarPanelCapacity)
            _solarPanels.Add(solarPanel);
    }

    public void AddBattery(GameObject battery)
    {
        if (battery.GetComponent<Battery>() == null)
            throw new Exception($"Mate {battery.name} is not a Battery!");

        _batteries.Add(battery);

        _energyCapacity += battery.GetComponent<Battery>().Capacity;
    }

    public void AddElectricalBox(GameObject electricalBox)
    {
        if (electricalBox.GetComponent<ElectricalBox>() == null)
            throw new Exception($"Mate {electricalBox.name} is not a ElectricalBox!");

        _electricBoxes.Add(electricalBox);

        _solarPanelCapacity += electricalBox.GetComponent<ElectricalBox>().SolarPanelCapacity;
    }

    public float GetEnergy(float desiredAmount)
    {
        desiredAmount = Mathf.Clamp(desiredAmount, 0f, _storedEnergy);

        _storedEnergy -= desiredAmount;

        _currentEnergyOutput += desiredAmount;

        return desiredAmount;
    }
}
