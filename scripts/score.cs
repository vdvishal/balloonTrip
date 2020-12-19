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
    public TMP_Text continueGameCoins;

    public GameObject gameOver;

    public int count;
 

    private void Start()
    {
 

    }

    private void OnEnable()
    {
 
        //   gameManager.Score = 0;
        if(SceneManager.GetActiveScene().buildIndex == 5)
        {
            HighScore.SetText(gameManager.instance.HighScore.ToString());
        }

        if(SceneManager.GetActiveScene().buildIndex == 4)
        {
            HighScore.SetText(gameManager.instance.balloonPopHighScore.ToString());
        }

        Score.SetText(gameManager.instance.Score.ToString());
        
        FlightTime.SetText(string.Format("{0:#0.0} Seconds", gameManager.instance.FlightTime));
        LongestFlightTime.SetText(string.Format("{0:#0.0} Seconds", gameManager.instance.LongestFlightTime));

        CloudVariables.BalloonCoins += gameManager.instance.Score;
        Cloud.Storage.Save();

        if (gameManager.instance.Score >= gameManager.instance.HighScore && SceneManager.GetActiveScene().buildIndex == 5)
        {
            CloudOnceServices.instance.submitScoreToLeaderBoard(gameManager.instance.Score);
        }

        if (gameManager.instance.Score >= gameManager.instance.balloonPopHighScore && SceneManager.GetActiveScene().buildIndex == 4)
        {
            CloudOnceServices.instance.submitBalloonPopToLeaderBoard(gameManager.instance.Score);
        }

        if (gameManager.instance.FlightTime >= gameManager.instance.LongestFlightTime && SceneManager.GetActiveScene().buildIndex == 5)
        {
            float time = gameManager.instance.FlightTime * 1000;

            CloudOnceServices.instance.submitFlightTimeToLeaderBoard((int)time);
        }

        CloudOnceServices.instance.updateScore();

    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            HighScore.SetText(gameManager.instance.HighScore.ToString());
        }

        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            HighScore.SetText(gameManager.instance.balloonPopHighScore.ToString());
        }

        Score.SetText(gameManager.instance.Score.ToString());

        FlightTime.SetText(string.Format("{0:#0.0} Seconds", gameManager.instance.FlightTime));
        LongestFlightTime.SetText(string.Format("{0:#0.0} Seconds", gameManager.instance.LongestFlightTime));

        if(gameManager.instance.balloonsCoins > 0)
        {
            continueGameCoins.SetText("Use 100 Balloons to Continue");

        }else
        {
            continueGameCoins.SetText("Not enough balloons");
        }

    }

}
