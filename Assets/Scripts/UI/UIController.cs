using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject shopBar;
    public GameObject battery;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleShopBar()
    {
        float shopBarHeight = shopBar.GetComponent<RectTransform>().rect.height / 1.65f ;

        RectTransform battRectTransform = battery.GetComponent<RectTransform>();
        if (!shopBar.activeSelf)
        {
            Debug.Log($"BattY: {battRectTransform.position.y} | ShopBarHeight: {shopBarHeight}");
            battRectTransform.position = new Vector3(battRectTransform.position.x, battRectTransform.position.y + shopBarHeight);
            Debug.Log($"BattY: {battRectTransform.position.y} | ShopBarHeight: {shopBarHeight}");
            shopBar.SetActive(true);
        }
        else
        {
            battRectTransform.position = new Vector3(battRectTransform.position.x, battRectTransform.position.y - shopBarHeight);
            shopBar.SetActive(false);
        }
    }

    private int charge = 0;
    public void GoBatteryUp()
    {
        BatteryController.Instance.SetCharge(++charge);
    }

    public void GoBatteryDown()
    {
        BatteryController.Instance.SetCharge(--charge);
    }

}
