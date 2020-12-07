using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
using TMPro;

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
        Score.SetText(gameManager.Score.ToString());
        HighScore.SetText(gameManager.HighScore.ToString());
        FlightTime.SetText(string.Format("{0:#0.0} Seconds", gameManager.FlightTime));
        LongestFlightTime.SetText(string.Format("{0:#0.0} Seconds", gameManager.LongestFlightTime));

    }
 
}
