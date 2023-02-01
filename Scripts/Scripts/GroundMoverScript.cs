using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMoverScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Invoke("MoveGround", 0.5f);             //Nessun tag esplicito da riconoscere poichè all'interno passerà solamente una pallina, Invoke per ritaradre invocazione.
    }

    public void MoveGround()
    {
        gameObject.transform.parent.position = new Vector3(0, 0, gameObject.transform.position.z + 70f);        //Metodo per spostare l'oggetto parent dell'oggetto che contiene questo script.
    }


}



//InvokeRepeating ripete un invoke all'infinito.
//CancelInvoke("-NomeMetodoInvocato-")   per fermare l'invoke.