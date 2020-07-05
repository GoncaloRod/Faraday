using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingPadManager : MonoBehaviour
{
    #region Singleton

    private static ChangingPadManager _instance;

    public static ChangingPadManager Instance => _instance;

    #endregion

    private List<GameObject> _chargingPads = new List<GameObject>();

    public int TotalSlots => _chargingPads.Count;

    public int SlotsInUse
    {
        get
        {
            int c = 0;

            foreach (GameObject chargingPadObj in _chargingPads)
            {
                if (!chargingPadObj.GetComponent<ChargingPad>().Available)
                    c++;
            }

            return c;
        }
    }

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
    }

    public void AddChargingPad(GameObject chargingPad)
    {
        if (chargingPad.GetComponent<ChargingPad>() == null)
            throw new Exception($"Mate {chargingPad.name} is not a ChargingPad!");

        _chargingPads.Add(chargingPad);
    }

    public ChargingPad GetChargingPad()
    {
        foreach (GameObject chargingPadObj in _chargingPads)
        {
            ChargingPad chargingPad = chargingPadObj.GetComponent<ChargingPad>();

            if (chargingPad.Available)
                return chargingPad;
        }

        return null;
    }
}
