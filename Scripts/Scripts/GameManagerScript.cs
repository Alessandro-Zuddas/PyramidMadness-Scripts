using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    //Primissima cosa che viene lanciata
    private void Awake()
    {
        Shader.SetGlobalFloat("_Curvature", 2.0f);
        Shader.SetGlobalFloat("_Trimming", 0.1f);
    }


    //Parte quando il gioco viene lanciato
    void Start()
    {
        
    }

    //Viene lanciato ad ogni frame
    void Update()
    {
        
    }
}
