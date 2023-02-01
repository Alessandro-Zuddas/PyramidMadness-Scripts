using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;           //Riferimento a gameobject player
    float offset;                       //Distanza tra palla e telecamera(offset = numero intero fisso tra due corpi).



    private void Start()
    {
        offset = player.transform.position.z - transform.position.z;                //Valore tra la pos z della proprietà transform di player e sottrazzione con val telecamera. 
    }



    private void LateUpdate()
    {
        //Cambiare posizione oggetto    //Lerp per movimenti delicati.
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(0, gameObject.transform.position.y, player.gameObject.transform.position.z - offset), Time.deltaTime * 100);
    }
}
