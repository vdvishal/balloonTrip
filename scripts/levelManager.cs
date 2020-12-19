using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelManager : MonoBehaviour
{
 
    public GameObject gameOver;
    public GameObject continueGame;
    public GameObject player;
    public GameObject platform;
    public TMP_Text score;

    public Transform platformTransform;
    public Transform playerTransform;

    public float startTime;

    bool boost;
    bool moveRightBool;
    bool moveLeftBool;
    float power = 150;
    float moveSpeed = 2500;
    GameObject playerPref;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {

        gameOver.SetActive(false);
        startTime = Time.time;
        gameManager.instance.playerDied = false;
        gameManager.instance.continueGame = true;

        gameManager.instance.FlightTime = 0;
        gameManager.instance.Score = 0;

        playerPref = Instantiate(player, new Vector3(playerTransform.position.x, playerTransform.position.y, 0f), Quaternion.identity);

        if (SceneManager.GetActiveScene().buildIndex == 5) {
            GameObject platPref = Instantiate(platform, new Vector3(platformTransform.position.x, platformTransform.position.y, 0f), Quaternion.identity);
            platPref.transform.SetParent(null);
        }

        playerPref.transform.SetParent(null);

        playerPref = GameObject.FindGameObjectWithTag("Player");

        anim = playerPref.transform.GetChild(0).GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.H))
        {
            accelerate();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            moveLeft();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            moveRight();
        }

        if (!gameManager.instance.playerDied)
        {
            gameManager.instance.FlightTime = Time.time - startTime;

            if (gameManager.instance.LongestFlightTime < gameManager.instance.FlightTime)
            {
                gameManager.instance.LongestFlightTime = gameManager.instance.FlightTime;
            }
        }
/*
        if (gameManager.instance.playerDied && gameManager.instance.continueGame)
        {
            continueGame.SetActive(true);
        }
        */
        if (gameManager.instance.playerDied && gameManager.instance.deathCount < 5  )
        {
            gameOver.SetActive(true);
        }

        if (gameManager.instance.playerDied && gameManager.instance.adsDeactivated  )
        {
            gameOver.SetActive(true);
        }

        score.SetText(gameManager.instance.Score.ToString());
    }

    public void reset(int type)
    {
        if(type == 1)
        {
           
            gameManager.instance.playerDied = false;
            gameManager.instance.continueGame = true;

            GameObject[] stars = GameObject.FindGameObjectsWithTag("star");
            continueGame.SetActive(false);

            playerPref = Instantiate(player, new Vector3(playerTransform.position.x, playerTransform.position.y,0f),Quaternion.identity);
            GameObject platPref = Instantiate(platform, new Vector3(platformTransform.position.x, platformTransform.position.y, 0f), Quaternion.identity);

            playerPref.transform.SetParent(null);
            platPref.transform.SetParent(null);
            playerPref = GameObject.FindGameObjectWithTag("Player");

            anim = playerPref.transform.GetChild(0).GetComponent<Animator>();

            foreach (GameObject star in stars)
            {
                Destroy(star);
            }
        }
        else
        {
          

            gameManager.instance.playerDied = false;
            gameManager.instance.continueGame = true;


            GameObject[] stars = GameObject.FindGameObjectsWithTag("star");
            continueGame.SetActive(false);

              playerPref = Instantiate(player, new Vector3(playerTransform.position.x, playerTransform.position.y, 0f),Quaternion.identity);
            GameObject platPref = Instantiate(platform, new Vector3(platformTransform.position.x, platformTransform.position.y, 0f), Quaternion.identity);

            playerPref.transform.SetParent(null);
            platPref.transform.SetParent(null);
            playerPref = GameObject.FindGameObjectWithTag("Player");

            anim = playerPref.transform.GetChild(0).GetComponent<Animator>();
            foreach (GameObject star in stars)
            {
                Destroy(star);
            }
        }
        
    }


    public void cancel()
    {
        gameManager.instance.playerDied = true;
        gameManager.instance.continueGame = false;

        continueGame.SetActive(false);
        gameOver.SetActive(true);

    }


    public void moveLeft()
    {
        if (!gameManager.instance.playerDied)
            playerPref.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1 * moveSpeed * Time.deltaTime, 0));

    }

    public void moveRight()
    {

        if (!gameManager.instance.playerDied)
            playerPref.GetComponent<Rigidbody2D>().AddForce(new Vector2(1 * moveSpeed * Time.deltaTime, 0));

    }

    public void moveStop()
    {
        moveRightBool = false;
        moveLeftBool = false;
    }

    public void accelerate()
    {

        if (!gameManager.instance.playerDied)
        {
            playerPref.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, power * 1f));
            anim.SetTrigger("boost");
            audioManager.instance.Play("fly");
        }

    }

    public void deaccelerate()
    {
        boost = false;
    }



    public void playerHit()
    {
        gameManager.instance.playerDied = true;
        anim.SetTrigger("shock");
        if (gameManager.instance.playerDied && !gameManager.instance.continueGame)
        {
            gameManager.instance.deathCount += 1;
            if (gameManager.instance.deathCount >= 5 && !gameManager.instance.adsDeactivated)
            {
                Adsmanager.instance.GameOver();
            }
        }

        //   player.transform.GetChild(0).SetParent(null);
    }

   




}
