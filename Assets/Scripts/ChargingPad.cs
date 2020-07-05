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

    public Transform EntryPoint;

    public bool Available => !_charging;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (_charging)
        {
            float desiredAmount = Mathf.Clamp(chargingSpeed * Time.deltaTime, 0f, _carCharging.MaxCapacity - _carCharging.currentEnergy);

            float amountToCharge = PowerSystemManager.Instance.GetEnergy(desiredAmount);

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

    private void OnDrawGizmosSelected()
    {
        if (EntryPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, .5f);
        Gizmos.DrawSphere(EntryPoint.position, .5f);
    }
}
