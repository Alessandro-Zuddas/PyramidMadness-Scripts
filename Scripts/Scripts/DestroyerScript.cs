using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerScript : MonoBehaviour
{
    private GameObject player;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    
    void Update()
    {
        if (gameObject.transform.position.z < player.transform.position.z - 20)                 //Distruggere oggetti che la pallina ha gia passato dopo un tot.
            Destroy(gameObject);
    }
}
