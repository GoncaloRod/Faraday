using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSystemManager : MonoBehaviour
{
    public GameObject[] Batteries;
    public float powerMade;
    public float powerUsed = 0;
    public float powerAvailable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        powerMade = TotalPower();
        powerAvailable = powerMade + powerUsed;
    }

    public float TotalPower()
    {       
        float total = 0;
        for (int c = 0; c < Batteries.Length; c++ ) { 
            total +=  (float)System.Math.Round(Batteries[c].GetComponent<BatteryManager>().energyStored, 2);
        }
        return total;
    }
}
