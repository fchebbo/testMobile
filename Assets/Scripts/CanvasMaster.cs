using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasMaster : MonoBehaviour
{
    public static CanvasMaster instance;

    public GameObject GameOverPanel;
    public GameObject VictoryPanel;
    public AudioSource gameOverMusic;
    public AudioSource victoryMusic;

    //TODO: USE MUTATOR PATTERN HERE
    public bool isGameOver;
    public bool isVictory;

    private AudioSource[] allAudioSources; 
    public Text score;

    // Catacombs music plays...
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        isGameOver = false;
        isVictory = false;
    }

    public void stopAllMusic()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Stop();
        }
    }

    private void FixedUpdate()
    {

    }

    public bool getGameOver()
    {
        return isGameOver;
    }

    public void playGameOverMusic()
    {
        gameOverMusic.Play();
    }

    public void setGameOver(bool gameOverState)
    {
        isGameOver = gameOverState;

    }

    public void doGameOver()
    {
        isGameOver = true;
        Invoke("showGameOverPanel", 1.5f);
    }

    public void doVictory()
    {
        isVictory = true;
        Invoke("showVictoryPanel", 1.5f);
    }
    public void showGameOverPanel()
    {
        playGameOverMusic();
        GameOverPanel.SetActive(true);
    }

    public void showVictoryPanel()
    {
        playVictoryMusic();
        VictoryPanel.SetActive(true);

    }
    public void playVictoryMusic()
    {
        victoryMusic.Play();
    }

    public bool getVictory()
    {
        return isVictory;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToGameScene ()
    {
        SceneManager.LoadScene("RingDodger");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }

}
