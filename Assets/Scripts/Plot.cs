using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class Slot
{
    public Vector2Int GridPosition { get; }

    public Vector3 WorldPosition { get; }

    public bool IsAvailable { get; set; }

    public Slot(Vector2Int gridPos, Vector3 worldPos, bool available = true)
    {
        GridPosition = gridPos;
        WorldPosition = worldPos;
        IsAvailable = available;
    }
}

public class Plot : MonoBehaviour
{
    [SerializeField] private Transform gridStart = null;

    [SerializeField] private Vector2 cellSize = Vector2.one;

    [SerializeField] private Vector2Int size = new Vector2Int(10, 10);

    public Slot[,] Grid { get; private set; }

    public Vector2Int Size => size;

    private void Awake()
    {
        Grid = new Slot[size.y, size.x];

        Vector3 gridStartPosition = gridStart.position;

        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                Grid[y, x] = new Slot(new Vector2Int(x, y), gridStartPosition + new Vector3(x * cellSize.x, 0f, y * cellSize.y));
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (gridStart == null)
            return;

        Vector3 gridStartPosition = gridStart.position;

        for (int row = 0; row < size.y; row++)
        {
            for (int column = 0; column < size.x; column++)
            {
                Gizmos.DrawWireSphere(gridStartPosition + new Vector3(column * cellSize.x, 0f, row * cellSize.y), .25f);
            }
        }
    }

    public Slot GetNearestSlot(Vector3 position)
    {
        Vector3 localPosition = gridStart.InverseTransformPoint(position);

        int row = Mathf.RoundToInt(localPosition.z / cellSize.y);
        int column = Mathf.RoundToInt(localPosition.x / cellSize.x);

        row = Mathf.Clamp(row, 0, size.y);
        column = Mathf.Clamp(column, 0, size.x);

        return Grid[row, column];
    }
}
