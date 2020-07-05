﻿using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildManager : MonoBehaviour
{
    #region Singleton
    
    private static BuildManager _instance;

    public static BuildManager Instance => _instance;

    #endregion

    private InputActions _inputActions;
    
    private Camera _camera;

    // This disappear once UI is done
    [Header("Prefabs")]
    [SerializeField] private GameObject batteryPrefab;
    [SerializeField] private GameObject solarPanelPrefab;
    [SerializeField] private GameObject electricalBoxPrefab;
    [SerializeField] private GameObject chargingStationPrefab;

    [SerializeField] private float initialBalance = 1000f;

    public GameObject currentlyBuilding = null;

    public float Balance;

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

        _inputActions = new InputActions();

        _camera = Camera.main;

        _inputActions.Building.Build.performed += ctx => Build();
    }

    private void Start()
    {
        Balance = initialBalance;
    }

    private void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            currentlyBuilding = solarPanelPrefab;
        }
        else if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            currentlyBuilding = electricalBoxPrefab;
        }
        else if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            currentlyBuilding = batteryPrefab;
        }
        else if (Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            currentlyBuilding = solarPanelPrefab;
        }
        else if (Keyboard.current.digit0Key.wasPressedThisFrame)
        {
            currentlyBuilding = null;
        }
    }

    private void Build()
    {
        if (currentlyBuilding == null)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Plot plot = hit.transform.GetComponentInParent<Plot>();

            if (plot != null)
            {
                Building building = currentlyBuilding.GetComponent<Building>();

                if (Balance < building.Cost)
                {
                    // TODO: Sound effect

                    return;
                }

                Slot slot = plot.GetNearestSlot(hit.point);

                if (building == null)
                    throw new Exception($"Mate '{currentlyBuilding.name}' is not a building!");

                if (IsPositionValid(plot, slot, building.Size, out Vector2Int topRight, out Vector2Int bottomLeft))
                {
                    GameObject buildingObject = Instantiate(currentlyBuilding, slot.WorldPosition, Quaternion.identity);

                    Balance -= building.Cost;

                    // Mark slots as unavailable
                    for (int y = bottomLeft.y; y <= topRight.y; y++)
                    {
                        for (int x = bottomLeft.x; x <= topRight.x; x++)
                        {
                            plot.Grid[y, x].IsAvailable = false;
                        }
                    }

                    switch (building)
                    {
                        case SolarPanel solarPanel:
                            PowerSystemManager.Instance.AddSolarPanel(buildingObject);
                            break;
                        case Battery battery:
                            PowerSystemManager.Instance.AddBattery(buildingObject);
                            break;
                        case ElectricalBox electricalBox:
                            PowerSystemManager.Instance.AddElectricalBox(buildingObject);
                            break;
                    }
                }
            }
        }
    }

    private bool IsPositionValid(Plot plot, Slot slot, Vector2Int Size, out Vector2Int topRight, out Vector2Int bottomLeft)
    {
        topRight = slot.GridPosition + Size / 2;
        bottomLeft = slot.GridPosition - Size / 2;

        // Inside bounds
        bool isInsideBounds = true;

        isInsideBounds &= topRight.x <= plot.Size.x && topRight.y <= plot.Size.y;
        isInsideBounds &= bottomLeft.x >= 0 && bottomLeft.y >= 0;

        // Every slot available
        bool isEverySlotAvailable = true;

        for (int y = bottomLeft.y; y <= topRight.y; y++)
        {
            for (int x = bottomLeft.x; x <= topRight.x; x++)
            {
                isEverySlotAvailable &= plot.Grid[y, x].IsAvailable;
            }
        }

        return isInsideBounds && isEverySlotAvailable;
    }

    private void OnEnable()
    {
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }
}
