using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Building
{
    [Header("Battery Settings")]
    [SerializeField] private float capacity;

    public float Capacity => capacity;
}
