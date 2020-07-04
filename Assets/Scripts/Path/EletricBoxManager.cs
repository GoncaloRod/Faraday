using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EletricBoxManager : MonoBehaviour
{
    public GameObject[] SolarPanels;
    public float powerGenerated;
    public int genNumber;

    // Start is called before the first frame update
    void Start()
    {
        //SolarPanels = new GameObject[transform.childCount];
        //Debug.Log(transform.childCount);

        //SolarPanels = new GameObject[1];
        //Debug.Log (SolarPanels.Length);
        //GroupResize (transform.childCount, ref SolarPanels);
        //Debug.Log (SolarPanels.Length);
        //Debug.Log(transform.GetChild(0));
        genNumber = 1;
        //addSolarPanel(ref genNumber);

    }

    // Update is called once per frame
    void Update()
    {
        /*for (int c = 1; c < SolarPanels.Length; c++ ) {
            if(SolarPanels[c].activeSelf){
                genNumber += 1;
                Debug.Log("Active");
            }       
        }*/
        powerGenerated += (float)System.Math.Round(genNumber * 0.1f, 2);
    }

    public void GroupResize (int Size, ref GameObject[] Group)
    {
        
     GameObject[] temp = new GameObject[Size];
     for (int c = 0; c < Mathf.Min(Size, Group.Length); c++ ) {
         temp [c] = Group [c];
     }
     Group = temp;
    }

    public void addSolarPanel (ref int gen)
    {
        for (int c = 0; c < SolarPanels.Length; c++ ) {
            if(!SolarPanels[c].activeSelf){
                gen ++;
                SolarPanels[c].SetActive(true);
                break;
            }  
        }
    }
}
