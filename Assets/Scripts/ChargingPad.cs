using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingPad : MonoBehaviour
{
    private bool _charging = false;
    private Car _carCharging;

    [SerializeField] private float chargingSpeed = 1f;
    [SerializeField] private float pricePerWat = 2f;

    private void Update()
    {
        if (_charging)
        {
            float amountToCharge = PowerSystemManager.Instance.GetEnergy(chargingSpeed * Time.deltaTime);

            _carCharging.currentEnergy += amountToCharge;
            BuildManager.Instance.Balance += amountToCharge * pricePerWat;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Car car = other.GetComponent<Car>();

        if (car != null)
        {
            _charging = true;
            _carCharging = car;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _charging = false;
        _carCharging = null;
    }
}
