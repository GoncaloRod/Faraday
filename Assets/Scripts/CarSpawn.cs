using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawn : MonoBehaviour
{
    private float _timer;

    [SerializeField] private float spawnTime = 10f;
    [SerializeField] private GameObject[] carPrefabs;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= spawnTime)
        {
            ChargingPad pad = ChargingPadManager.Instance.GetChargingPad();

            if (ChargingPadManager.Instance.SlotsInUse < ChargingPadManager.Instance.TotalSlots)
            {
                if (carPrefabs.Length > 0)
                {
                    GameObject prefab = carPrefabs[(int) Random.Range(0, carPrefabs.Length)];
                    Instantiate(prefab, transform.position, prefab.transform.rotation);
                }
            }

            _timer = 0f;
        }
    }
}
