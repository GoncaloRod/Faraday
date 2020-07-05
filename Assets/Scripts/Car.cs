using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Car : MonoBehaviour
{
    private ChargingPad _destinationPad;
    private Transform _exit;
    private Quaternion _initalRotation;

    private Queue<Vector3> _getInPoints = new Queue<Vector3>();
    private Queue<Vector3> _getOutPoints = new Queue<Vector3>();

    private Vector3 _nextPoint;

    [SerializeField] private float speed = 10f;
    [SerializeField] private float batteryCapacity = 300;

    public float currentEnergy;

    public float MaxCapacity => batteryCapacity;

    private void Awake()
    {
        _exit = GameObject.FindGameObjectWithTag("Finish").transform;

        currentEnergy = Random.Range(20f, batteryCapacity - 50f);

        _destinationPad = ChargingPadManager.Instance.GetChargingPad();
        _destinationPad.Available = false;

        _getInPoints.Enqueue(new Vector3(transform.position.x, 0f, _destinationPad.EntryPoint.position.z));
        _getInPoints.Enqueue(_destinationPad.EntryPoint.position);
        _getInPoints.Enqueue(_destinationPad.transform.position);

        _getOutPoints.Enqueue(_destinationPad.EntryPoint.position);
        _getOutPoints.Enqueue(new Vector3(_exit.position.x, 0f, _destinationPad.EntryPoint.position.z));
        _getOutPoints.Enqueue(_exit.position);

        _nextPoint = _getInPoints.Dequeue();

        _initalRotation = transform.rotation;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _nextPoint, Time.deltaTime * speed);

        if (transform.position == _nextPoint)
        {
            if (_getInPoints.Count > 0)
            {
                _nextPoint = _getInPoints.Dequeue();
            }

            if (currentEnergy == batteryCapacity && _getOutPoints.Count > 0)
            {
                _nextPoint = _getOutPoints.Dequeue();
            }
        }
        else
        {
            transform.LookAt(_nextPoint);
            transform.Rotate(_initalRotation.eulerAngles);
        }

        if (transform.position == _exit.position)
            Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        foreach (Vector3 inPoint in _getInPoints)
        {
            Gizmos.DrawSphere(inPoint, .5f);
        }

        foreach (Vector3 outPoint in _getOutPoints)
        {
            Gizmos.DrawSphere(outPoint, .5f);
        }
    }
}
