using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Car : MonoBehaviour
{
    [SerializeField] private float batteryCapacity = 300;

    [HideInInspector] public float currentEnergy;

    private void Awake()
    {
        currentEnergy = Random.Range(20f, batteryCapacity);
    }
}
