using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxScript : MonoBehaviour
{
    public int score;

    public Material skyOne;
    public Material skyTwo;
    public Material skyThree;
    
    
   



    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSky();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Score")
        {
            score++;                                        //Aumento il valore di score quando il game object che contiene lo script tocca il gameobject con tag score.
        }
    }


    void ChangeSky()
    {
        if (score < 25)
        {
            RenderSettings.skybox = skyOne;
        }
        else if(score < 50)
        {
            RenderSettings.skybox = skyTwo;
        }
        else
        {
            RenderSettings.skybox = skyThree;
        }
    }



    
}
