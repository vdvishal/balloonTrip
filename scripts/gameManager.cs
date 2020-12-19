using CloudOnce;
using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public static gameManager instance = null;

    public int[] cosmeticsArr;
    public int cosmeticsSelectedNum = -1;

 


    public int deathCount;
    public bool playerDied;
    public bool adsDeactivated;

    public Sprite selectedBalloon;

    public GameObject[] sprites;

    public GameObject[] unlocked;


    public Sprite[] spritesBalloon;

    public Sprite defaultBalloon;

    public bool continueGame;
    // cloud variables
    public int balloonsCoins;

    public int Score;
    public int HighScore;
    public int balloonPopHighScore;

    
    public float FlightTime;
    public float LongestFlightTime;
    public int IAP;

    public string unlockedString;

    public bool soundOn;

    private float waitTime = 10f;
    private bool waitCalled;
    private float waitStartTime;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else
        {
            Destroy(gameObject);
        }
        soundOn = true;
        continueGame = false;
        DontDestroyOnLoad(gameObject);
    }




    // Start is called before the first frame update
    void Start()
    {
        deathCount = 0;
        adsDeactivated = false;
        Score = 0;
        cosmeticsArr = new int[cosmeticsArr.Length];
        cosmeticsSelectedNum = -1;
        selectedBalloon = defaultBalloon;
        Cloud.OnInitializeComplete += CloudInitComplete;
        Cloud.OnCloudLoadComplete += CloudLoadComplete;
        Cloud.Initialize(true, true);
        waitStartTime = Time.time;
        continueGame = false;

    }

    private void Update()
    {
        if(Time.time - waitStartTime > waitTime && !waitCalled)
        {
            mainMenu();
            waitCalled = true;
        }
    }

    public void CloudInitComplete()
    {
        Cloud.OnInitializeComplete -= CloudInitComplete;
        mainMenu();
        waitCalled = true;
        Cloud.Storage.Load();
    }

    void CloudLoadComplete(bool success)
    {

            Cloud.OnCloudLoadComplete -= CloudLoadComplete;

            balloonsCoins = CloudVariables.BalloonCoins;
            HighScore = CloudVariables.HighScores;
            unlockedString = CloudVariables.unlocked;
            LongestFlightTime = CloudVariables.HighestFlightTime;
            balloonPopHighScore = CloudVariables.balloonPopHighScores;
            IAP = CloudVariables.IAP;

        if(IAP == 1 || IAP == 3)
        {
            adsDeactivated = true;
        }

        if (CloudVariables.selectedBalloon > -1)
            {
                selectedBalloon = spritesBalloon[CloudVariables.selectedBalloon];
            }
            else
            {
                selectedBalloon = defaultBalloon;
            }

        updateUnlocks();



    }

    public void updateUnlocks()
    {

        string[] words = unlockedString.Split(',');
        int index = 0;
        foreach (var word in words)
        {
            if (index != 0)
            {
                int num = int.Parse(word);
                cosmeticsArr[num] = 1;
                Array.Resize(ref unlocked, unlocked.Length + 1);
                unlocked[unlocked.GetUpperBound(0)] = sprites[num];
            }
            index++;
        }
    }

    public void startGame()
    {
        audioManager.instance.Play("tran");
        SceneManager.LoadScene(2);
 

    }

    public void loadIAP()
    {
        audioManager.instance.Play("tran");
        SceneManager.LoadScene(3);
 

    }

    public void ChangeScene(int num)
    {
        audioManager.instance.Play("tran");
        SceneManager.LoadScene(num);
 

    }


    public void restartGame()
    {

 

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        FlightTime = 0;
        Score = 0;
    }

    public void soundToggle()
    {
        soundOn = !soundOn;
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(1);
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

    public void unlock(int num)
    {
        cosmeticsArr[num] = 1;
        Array.Resize(ref unlocked, unlocked.Length + 1);
        unlocked[unlocked.GetUpperBound(0)] = sprites[num];
    }

    public void select(int num)
    {
        selectedBalloon = spritesBalloon[num];

        cosmeticsSelectedNum = num;
        CloudVariables.selectedBalloon = num;
        Cloud.Storage.Save();
    }


    private void save()
    {

    }

    
}
