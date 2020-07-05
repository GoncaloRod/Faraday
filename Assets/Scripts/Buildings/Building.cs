using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [Header("Building Settings")]
    [SerializeField] private string name = "";
    [SerializeField] private Sprite icon;
    [SerializeField] private float cost = 100;

    [SerializeField] private Vector2Int size = Vector2Int.one;

    public string Name => name;
    public Sprite Icon => icon;
    public float Cost => cost;
    public Vector2Int Size => size;
}
