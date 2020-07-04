using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalBox : Building
{
    [Header("Electrical Box Settings")]
    [SerializeField] private int solarPanelCapacity = 6;

    public int SolarPanelCapacity => solarPanelCapacity;
}
