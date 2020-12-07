using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public static gameManager instance = null;

    public int Score;
    public int HighScore;

    public float FlightTime;
    public float LongestFlightTime;

    public int deathCount;
    public bool playerDied;
    public bool adsDeactivated;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        deathCount = 0;
        adsDeactivated = false;
        Score = 0;
    }
    
    public void startGame()
    {
        SceneManager.LoadScene(1);
    }

    public void restartGame()
    {
        SceneManager.LoadScene(1);
        FlightTime = 0;
        Score = 0;
    }

    public void changeScene()
    {

    }

    public void mainMenu()
    {
        SceneManager.LoadScene(0);
        FlightTime = 0;
        Score = 0;
    }

    public void updateScore()
    {
        Score += 1;

        if(HighScore < Score)
        {
            HighScore = Score;
        }


    }

}
