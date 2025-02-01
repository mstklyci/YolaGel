using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawn : MonoBehaviour
{
    [SerializeField] private Transform[] spawnTransforms;
    private int spawnPlace = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Car")
        {
           

            Transform spawnPoint = spawnTransforms[spawnPlace];
            other.gameObject.transform.position = spawnPoint.position;

            CarAI carAI = other.gameObject.GetComponent<CarAI>();
            if(carAI != null) { carAI.SelectRoad(spawnPlace); };

            if (spawnPlace == 1)
            {
                spawnPlace = 0;
            }
            else
            {
                spawnPlace = 1; 
            }
        }   
    }
}