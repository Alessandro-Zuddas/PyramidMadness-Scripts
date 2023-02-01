using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnerScript : MonoBehaviour
{
    public GameObject player;
    public GameObject[] obstaclePrefabs;
    private Vector3 spawnPosition;
    bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.gameObject.transform.position, spawnPosition);

        if(distance < 200)
        {
            SpawnObstacle();
        }
    }


    void SpawnObstacle()
    {
        if(!started)
        {
            spawnPosition = new Vector3(0, (float)1, spawnPosition.z + 50);
            started = true;
        }
        else
         spawnPosition = new Vector3(0, (float)1, spawnPosition.z + 25);
        
        //int randomNum = Random.Range(0, 4);         //Estrarre numero random da passare a istantiate.

        //Quaternion indica la rotazione.
        Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], spawnPosition, Quaternion.identity);
    }


}
