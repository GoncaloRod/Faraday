using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryManager : MonoBehaviour
{
    public GameObject[] ElectricalBoxes;
    public float energyStored;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        energyStored = TotalEnergy();
    }

    public float TotalEnergy()
    {       
        float total = 0;
        for (int c = 0; c < ElectricalBoxes.Length; c++ ) { 
            total +=  (float)System.Math.Round(ElectricalBoxes[c].GetComponent<EletricBoxManager>().powerGenerated, 2);
        }
        return total;
    }
}
