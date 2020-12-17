using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using CloudOnce;
using UnityEngine.SceneManagement;

public class score : MonoBehaviour
{

    public TMP_Text Score;
    public TMP_Text HighScore;
    public TMP_Text FlightTime;
    public TMP_Text LongestFlightTime;

    public GameObject gameOver;

    public int count;
    private gameManager gameManager;


    private void Start()
    {
        gameManager = FindObjectOfType<gameManager>();
     //   gameManager.Score = 0;
 

    }

    private void OnEnable()
    {
        Debug.Log("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
        gameManager = FindObjectOfType<gameManager>();
        //   gameManager.Score = 0;
        if(SceneManager.GetActiveScene().buildIndex == 5)
        {
            HighScore.SetText(gameManager.HighScore.ToString());
        }

        if(SceneManager.GetActiveScene().buildIndex == 4)
        {
            HighScore.SetText(gameManager.balloonPopHighScore.ToString());
        }

        Score.SetText(gameManager.Score.ToString());
        
        FlightTime.SetText(string.Format("{0:#0.0} Seconds", gameManager.FlightTime));
        LongestFlightTime.SetText(string.Format("{0:#0.0} Seconds", gameManager.LongestFlightTime));

        CloudVariables.BalloonCoins += gameManager.Score;
        Cloud.Storage.Save();

        if (gameManager.Score >= gameManager.HighScore && SceneManager.GetActiveScene().buildIndex == 5)
        {
            CloudOnceServices.instance.submitScoreToLeaderBoard(gameManager.Score);
        }

        if (gameManager.Score >= gameManager.HighScore && SceneManager.GetActiveScene().buildIndex == 4)
        {
            CloudOnceServices.instance.submitBalloonPopToLeaderBoard(gameManager.Score);
        }

        if (gameManager.FlightTime >= gameManager.LongestFlightTime && SceneManager.GetActiveScene().buildIndex == 5)
        {
            float time = gameManager.FlightTime * 1000;

            CloudOnceServices.instance.submitFlightTimeToLeaderBoard((int)time);
        }

        CloudOnceServices.instance.updateScore();

    }

}
