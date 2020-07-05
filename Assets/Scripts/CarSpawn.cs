using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawn : MonoBehaviour
{
    private float _timer;

    [SerializeField] private GameObject[] carPrefabs;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= 5f)
        {
            ChargingPad pad = ChargingPadManager.Instance.GetChargingPad();

            if (pad != null)
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
