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

    public float CurrentEnergyInput => _currentEnergyInput;
    
    public float CurrentEnergyOutput => _currentEnergyOutput;

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
        foreach (GameObject solarPanel in _solarPanels)
        {
            _currentEnergyInput += solarPanel.GetComponent<SolarPanel>().CurrentOutput * Time.deltaTime;
        }

        if (_storedEnergy + _currentEnergyInput > _energyCapacity)
        {
            _storedEnergy = _energyCapacity;
        }
        else
        {
            _storedEnergy += _currentEnergyInput;
        }
    }

    public void AddSolarPanel(GameObject solarPanel)
    {
        if (_solarPanels.Count < _solarPanelCapacity)
            _solarPanels.Add(solarPanel);
    }

    public void AddBattery(GameObject battery)
    {
        _batteries.Add(battery);

        _energyCapacity += battery.GetComponent<Battery>().Capacity;
    }

    public void AddElectricalBox(GameObject electricalBox)
    {
        _electricBoxes.Add(electricalBox);

        _solarPanelCapacity += electricalBox.GetComponent<ElectricalBox>().SolarPanelCapacity;
    }
}
