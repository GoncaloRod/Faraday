using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildManager : MonoBehaviour
{
    private InputActions _inputActions;
    
    private Camera _camera;

    [SerializeField] private LayerMask groundLayer;

    [Header("Prefabs")]
    [SerializeField] private GameObject batteryPrefab;
    [SerializeField] private GameObject solarPanelPrefab;
    [SerializeField] private GameObject electricalBoxPrefab;
    [SerializeField] private GameObject chargingStationPrefab;

    public GameObject currentlyBuilding = null;

    private void Awake()
    {
        _inputActions = new InputActions();

        _camera = Camera.main;

        _inputActions.Building.Build.performed += ctx => Build();
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
                Slot slot = plot.GetNearestSlot(hit.point);

                Building building = currentlyBuilding.GetComponent<Building>();

                if (building == null)
                    throw new Exception($"Mate '{currentlyBuilding.name}' is not a building!");

                if (IsPositionValid(plot, slot, building.Size, out Vector2Int topRight, out Vector2Int bottomLeft))
                {
                    Instantiate(currentlyBuilding, slot.WorldPosition, Quaternion.identity);

                    // Mark slots as unavailable
                    for (int y = bottomLeft.y; y <= topRight.y; y++)
                    {
                        for (int x = bottomLeft.x; x <= topRight.x; x++)
                        {
                            plot.Grid[y, x].IsAvailable = false;
                        }
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
