using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public GameObject buildPrefab;

    public TextMeshProUGUI priceCost;

    public Button buttonImage;
    // Start is called before the first frame update
    private Building _building;

    void Start()
    {
        _building = buildPrefab.GetComponent<Building>();
        priceCost.SetText($"Cost: {_building.Cost}");
        buttonImage.image.sprite = _building.Icon;
    }

    public void BuyBuilding()
    {
        if(BuildManager.Instance.Balance >= _building.Cost)
            BuildManager.Instance.currentlyBuilding = buildPrefab;
    }

    public void BuyChargeStation()
    {
        if (BuildManager.Instance.Balance >= _building.Cost)
            BuildManager.Instance.BuildChargingStation(buildPrefab);
    }

}
