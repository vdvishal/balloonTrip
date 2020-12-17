using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CloudOnce;
using UnityEngine.SceneManagement;

public class CloudOnceServices : MonoBehaviour
{
    public static CloudOnceServices instance;
 

    private void Awake()
    {
        testSignleton();
    }

    void testSignleton()
    {
        if (instance != null)
        {
            Destroy(gameObject); return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void submitScoreToLeaderBoard(int score)
    {
        Leaderboards.Score.SubmitScore(score);
    }

    public void submitFlightTimeToLeaderBoard(long time)
    {
        Leaderboards.FlightTime.SubmitScore(time);
    }

    public void submitBalloonPopToLeaderBoard(int score)
    {
        Leaderboards.balloonPop.SubmitScore(score);
    }

    public void updateScore()
    {
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            CloudVariables.HighScores = gameManager.instance.Score;
            CloudVariables.HighestFlightTime = gameManager.instance.FlightTime;
            Cloud.Storage.Save();
        }else
        {
            CloudVariables.balloonPopHighScores = gameManager.instance.Score;
            Cloud.Storage.Save();
        }

    }
}
