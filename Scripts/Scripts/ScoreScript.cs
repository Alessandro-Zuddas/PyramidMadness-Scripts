using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public int score;
    public Text scoreText;          //Dichiarazione variabile della label
    public Text highScoreText;
    public static ScoreScript current;


    private void Awake()
    {
        if(current == null)
        {
            current = this;                     //Se current è nulla la caarichiamo con this(la classe in cui siamo), rendiamo lo script leggibile da altri script.
        }
    }

    private void Start()
    {
        score = 0;
    }

    private void Update()
    {
        //Definizione scritta label
        scoreText.text = score.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Score")
        {
            score++;                                        //Aumento il valore di score quando il game object che contiene lo script tocca il gameobject con tag score.
        }
    }

    public void CheckHighScore()
    {
        if(PlayerPrefs.HasKey("highScore"))
        {
            if (score > PlayerPrefs.GetInt("highScore"))
                PlayerPrefs.SetInt("highScore", score);                     //PlayerPrefs è una variabile che ne contiene altrettante di diversi tipi e che consente di registrare dati sul telefono dell'utente.
        }
        else
        
            PlayerPrefs.SetInt("highScore", score);

            highScoreText.text = PlayerPrefs.GetInt("highScore").ToString();

        
    }
}
