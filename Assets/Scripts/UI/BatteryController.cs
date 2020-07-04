using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryController : MonoBehaviour
{
    #region Singleton

    private static BatteryController _instance;

    public static BatteryController Instance => _instance;

    #endregion

    public GameObject BarsObject;
    private GameObject[] _bars;

    private void Awake()
    {
        if (_instance != null && _instance != this) Destroy(gameObject);
        else _instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        int size = BarsObject.transform.childCount;
        _bars = new GameObject[size];

        for (int i = 0; i < size; ++i)
        {
            _bars[i] = BarsObject.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Value goes from 1 to 9
    public void SetCharge(int value)
    {
        for (int i = 0; i < _bars.Length; ++i)
        {
            GameObject gb = _bars[i];
            // Value - 1 it's converts the first bar of the battery to the first array element
            gb.SetActive(value - 1 >= i);
        }
    }
}
