using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarPanel : Building
{
    private float _lastClear = 0f;

    [Header("Solar Panel Settings")]
    [SerializeField] private float optimumPowerOutput = 100;

    public float CurrentOutput { get; private set; }

    private void Update()
    {
        CurrentOutput = optimumPowerOutput;
    }
}
