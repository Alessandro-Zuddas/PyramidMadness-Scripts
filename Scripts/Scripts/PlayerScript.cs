using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class PlayerScript : MonoBehaviour
{
    public static PlayerScript ricompensa;

    int gamesPlayed;

    public GameObject rewardButton;
    public GameObject reloadButton;


    public Rigidbody rigidBody;
    public AudioClip score;
    public AudioClip error;
    public Button audioButton;
    public Sprite audioOn;
    public Sprite audioOff;
    public Button pauseButton;
    public Sprite gameOn;
    public Sprite gameOff;

    public GameObject startPanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;

    public GameObject gameLabels;

    bool audioIsOn;
    bool gameIsOn;

    public Text scoreText;

    private float playerSpeed = 800;
    private float directionSpeed = 60;


    private void Awake()
    {
        if(ricompensa == null)
        {
            ricompensa = this;
        }
    }


    private void Start()
    {
      

        gameOverPanel.SetActive(false);
        gameLabels.SetActive(false);
        startPanel.SetActive(true);
        audioIsOn = true;
        audioButton.GetComponent<Image>().sprite = audioOn;                         //Prendiamo componente image e impostiamo alla proprietà audio on.
       

        gameIsOn = false;
        
        pausePanel.SetActive(false);

        rewardButton.GetComponent<Button>().enabled = false;                    //Prendiamo il componente button di rewarded button e impostiamo la proprietà enabled su false
        rewardButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);       //Rendiamo il componente image di reward button trasparente poichè non attivo.


        reloadButton.GetComponent<Button>().enabled = true;
        reloadButton.GetComponent<Image>().color = new Color(1, 1, 1, 1f);

        if (PlayerPrefs.HasKey("gamesPlayed"))
        {
            gamesPlayed = PlayerPrefs.GetInt("gamesPlayed");
        }
        else
            gamesPlayed = 0;
    }

    
    private void FixedUpdate()
    {
        //Se il gioco è nell'editor di unity allora....(sempre con #endif).
#if UNITY_EDITOR

        float moveHorizontal = Input.GetAxis("Horizontal");                                                //Dichiarato variabile dentro Update per maggiori prestazioni, raccolta input orizz.
        //Debug.Log("Input: " + moveHorizontal);                                                           //Mostra in una finestra di log quello che c'è dentro le parentesi tonde.

        //Operiamo sulla posizione dell'oggetto, v3.Lerp = incidere in uno o più vettori x,y,z con proprietà Lerp(movimento non repentino ma graduale) indicando la posizione in cui andare.
        transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(Mathf.Clamp(gameObject.transform.position.x + moveHorizontal, -2.5f, 2.5f), gameObject.transform.position.y, gameObject.transform.position.z), directionSpeed * Time.deltaTime);
#endif  //Chiusura condizione obbligatoria con #.
        if(gameIsOn)
           GetComponent<Rigidbody>().velocity = Vector3.forward * playerSpeed * Time.deltaTime;               //Prendiamo componente rigidbody di player, agisco nella proprietà velocity.


        //Situazione per smartphone con v2 perchè lo schermo è bidimensionale.
        Vector2 touch = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10f));           //Registrazione touch dell'utente sullo schermo attraverso la main camera(ScreenToWorldPoint).

        //Se tocchi maggiore di 0 e il primo tocco equivale alla strisciata sullo schermo.
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
         transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(Mathf.Clamp(touch.x, -2.5f, 2.5f), gameObject.transform.position.y, gameObject.transform.position.z), directionSpeed * Time.deltaTime);
        }

        ChangeSpeed();

    }






    public void StartGame()
    {
        gameIsOn = true;
        //startPanel.SetActive(false);
        startPanel.GetComponent<Animator>().Play("StartPanelAnimation");
        gameLabels.SetActive(true);

        //aggiungiamo 1 a games played
        gamesPlayed++;
        PlayerPrefs.SetInt("gamesPlayed", gamesPlayed);

        Debug.Log("We Played " + gamesPlayed + "games.");
    }









    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Score")
            GetComponent<AudioSource>().PlayOneShot(score, 1.0f);

        if (other.gameObject.tag == "Pyramid")
        {
           
            GameOver();
            
        }
            

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }





    void GameOver()
    {
        
        GetComponent<AudioSource>().PlayOneShot(error, 1.0f);
        gameOverPanel.SetActive(true);

        directionSpeed = 0;
        
        gameLabels.SetActive(false);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;   //Con freeze position blocchiamo la pallina su asse x,y,z.
        gameIsOn = false;

        scoreText.text = ScoreScript.current.score.ToString();

        ScoreScript.current.CheckHighScore();
        
        if(gamesPlayed % 3 == 0)
        {
            rewardButton.GetComponent<Button>().enabled = true;                    //Prendiamo il componente button di rewarded button e impostiamo la proprietà enabled su false
            rewardButton.GetComponent<Image>().color = new Color(1, 1, 1, 1f);       //Rendiamo il componente image di reward button trasparente poichè non attivo.


            reloadButton.GetComponent<Button>().enabled = false;
            reloadButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);

            reloadButton.GetComponent<Animator>().enabled = false;
            rewardButton.GetComponent<Animator>().enabled = true;
        }
        else
        {
            rewardButton.GetComponent<Button>().enabled = false;                    //Prendiamo il componente button di rewarded button e impostiamo la proprietà enabled su false
            rewardButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);       //Rendiamo il componente image di reward button trasparente poichè non attivo.


            reloadButton.GetComponent<Button>().enabled = true;
            reloadButton.GetComponent<Image>().color = new Color(1, 1, 1, 1f);

            rewardButton.GetComponent<Animator>().enabled = false;
            reloadButton.GetComponent<Animator>().enabled = true;
        }

        AdMobScript.current.GameOver();
    }






    public void StopMusic()
    {
        if(audioIsOn)
        {
            GetComponent<AudioSource>().Stop();
            audioIsOn = false;
            audioButton.GetComponent<Image>().sprite = audioOff;
        }
        else                                                                       //Ciclo per attivare e disattivare musica e cambiare icona.
        {
            GetComponent<AudioSource>().Play();
            audioIsOn = true;
            audioButton.GetComponent<Image>().sprite = audioOn;
        }
    }


    public void PauseGame()
    {
        if (gameIsOn)
        {
            directionSpeed = 0;
            pausePanel.SetActive(true);
            gameLabels.SetActive(false);
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;   //Con freeze position blocchiamo la pallina su asse x,y,z.
            gameIsOn = false;
            pauseButton.GetComponent<Image>().sprite = gameOff;

            GetComponent<AudioSource>().Pause();
        }
        else                                                                       //Ciclo per attivare e disattivare musica e cambiare icona.
        {
            directionSpeed = 80;
            //Invoke("RestartPlayer", 0.5f);
            pausePanel.SetActive(false);
            gameLabels.SetActive(true);
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;

            gameIsOn = true;
            pauseButton.GetComponent<Image>().sprite = gameOn;

            GetComponent<AudioSource>().Play();
        }
    }

    // void RestartPlayer()
    // {
    //   GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    //  GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
    //}

    void ChangeSpeed()
    {
        if (ScoreScript.current.score < 25)
        {
            playerSpeed = 1000;
            directionSpeed = 60;
        }
        else if (ScoreScript.current.score < 50)
        {
            playerSpeed = 1200;
            directionSpeed = 70;
        }
        else
        {
            playerSpeed = 1400;
            directionSpeed = 80;
        }
    }



}
