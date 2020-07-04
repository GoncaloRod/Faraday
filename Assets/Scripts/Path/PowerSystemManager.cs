using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSystemManager : MonoBehaviour
{


    private static PowerSystemManager _instance;
    public static PowerSystemManager Instance => _instance;   

    public float powerMade;
    public float powerUsed = 0;
    public float powerAvailable;
    public int maxSolarPanels = 0;
    public int maxEnergyCap = 0;

    public List<SolarPanel> SolarPanelsList = new List<SolarPanel>();
    public List<Battery> BatteriesList = new List<Battery>();
    public List<ElectricalBox> ElectricBoxesList = new List<ElectricalBox>();

    // Start is called before the first frame update
    private void Awake(){
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

    }

    // Update is called once per frame
    void Update(){
        powerAvailable = powerMade + powerUsed;
    }

    public void addEletricBox(){
        
    }

    public void addSolarPanel(){

    }

    public void addBattery(){
        
    }
}
