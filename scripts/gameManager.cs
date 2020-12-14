using CloudOnce;
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


    public Sprite[] spritesBalloon;

    public Sprite defaultBalloon;

    // cloud variables
    public int balloonsCoins;

    public int Score;
    public int HighScore;

    public float FlightTime;
    public float LongestFlightTime;


    private string unlockedString;

    public bool soundOn;



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
        DontDestroyOnLoad(gameObject);
    }




    // Start is called before the first frame update
    void Start()
    {
        deathCount = 0;
        adsDeactivated = false;
        Score = 0;
        cosmeticsArr = new int[27];
        cosmeticsSelectedNum = -1;
        selectedBalloon = defaultBalloon;
        Cloud.OnInitializeComplete += CloudInitComplete;
        Cloud.OnCloudLoadComplete += CloudLoadComplete;
        Cloud.Initialize(true, true);

    }

    public void CloudInitComplete()
    {
        Cloud.OnInitializeComplete -= CloudInitComplete;
        mainMenu();
        Cloud.Storage.Load();
    }

    void CloudLoadComplete(bool success)
    {

            Cloud.OnCloudLoadComplete -= CloudLoadComplete;

            balloonsCoins = CloudVariables.BalloonCoins;
            HighScore = CloudVariables.HighScores;
            unlockedString = CloudVariables.unlocked;
            LongestFlightTime = CloudVariables.HighestFlightTime;
            
            if(CloudVariables.selectedBalloon > -1)
            {
                selectedBalloon = spritesBalloon[CloudVariables.selectedBalloon];
            }
            else
            {
                selectedBalloon = defaultBalloon;
            }
        
            string[] words = unlockedString.Split(',');
            int index = 0;
            foreach (var word in words)
            {
                if(index != 0)
                {
                    int num = int.Parse(word);
                    cosmeticsArr[num] = 1;
                }

                index++;
            }

        

    }

    public void startGame()
    {
        SceneManager.LoadScene(2);
    }

    public void loadIAP()
    {
        SceneManager.LoadScene(3);
    }


    public void restartGame()
    {
        SceneManager.LoadScene(2);
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
        Debug.Log(cosmeticsArr.ToString());
        // save the element
    }

    public void select(int num)
    {
        selectedBalloon = spritesBalloon[num];

        cosmeticsSelectedNum = num;
    }



    
}
