using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarPanel : Building
{
    private float _lastClear = 0f;

    [Header("Solar Panel Settings")]
    [SerializeField] private float optimumPowerOutput = 100;

    [Header("Dirtiness Settings")]
    [SerializeField] private float timeToDirty = 10 * 60;
    [SerializeField] private float cleaningDownTime = 1 * 60;

    public float CurrentOutput { get; private set; }

    private void Update()
    {
        _lastClear += Time.deltaTime;

        float dirtyPercentage = Mathf.Clamp(_lastClear / timeToDirty, 0f, 1f);

        CurrentOutput = optimumPowerOutput * (1 - dirtyPercentage);
    }
}
