using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PathCreation.Examples
{
    public class CarCreation : MonoBehaviour
    {
        

        public GameObject myPrefab;
        private IEnumerator coroutine;
        private float timestamp = 0.0f;
        public float delay = 10.0f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (timestamp <= Time.time) {
            timestamp = Time.time + delay + (Time.time - timestamp);
            GameObject clone = Instantiate(myPrefab, myPrefab.transform.position, Quaternion.identity);
            clone.SetActive(true);
            clone.GetComponent<PathFollower>().enabled = true;
            clone.GetComponent<PathFollower>().speed = 5;
            clone.GetComponent<PathFollower>().fuel = 0;  
            }
        }
    }
}