using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ballon_movement : MonoBehaviour
{

    public Rigidbody2D player;

   //  public GameObject playerSkin;

 

    [HideInInspector]
    public audioManager audioMan;

    public Animator anim;

    public float power;
    public float moveSpeed;
    public float startTime;

    private gameManager gameManager;

    public GameObject gameOver;
    public TMP_Text text;

    //private bool death;

    float move_x;

    bool boost;
    bool moveRightBool;
    bool moveLeftBool;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<gameManager>();
        gameManager.playerDied = false;
        gameOver.SetActive(false);
        gameManager.FlightTime = 0;
        gameManager.Score = 0;

        startTime = Time.time;

        player.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = gameManager.instance.selectedBalloon;

    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.playerDied)
        {
            gameManager.FlightTime = Time.time - startTime;

            if (gameManager.LongestFlightTime < gameManager.FlightTime)
            {
                gameManager.LongestFlightTime = gameManager.FlightTime;
            }
        }


        if (Input.GetKeyDown(KeyCode.H)) {
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
 
        if (gameManager.playerDied && gameManager.instance.deathCount < 5)
        {
            gameOver.SetActive(true);
        }
        
        if (gameManager.playerDied && gameManager.instance.adsDeactivated)
        {
            gameOver.SetActive(true);
        }


        text.SetText(gameManager.Score.ToString());
       // Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, 0.5f);


        //move_x = Input.GetAxis("Horizontal");

    }

    public void moveLeft()
    {
        if (!gameManager.playerDied)
            player.AddForce(new Vector2(-1 * moveSpeed * Time.deltaTime, 0));
 
    }

    public void moveRight()
    {

        if (!gameManager.playerDied)
            player.AddForce(new Vector2(1 * moveSpeed * Time.deltaTime, 0));
   
    }

    public void moveStop()
    {
        moveRightBool = false;
        moveLeftBool = false;
    }

    public void accelerate()
    {
        Debug.Log(gameManager.playerDied);
        Debug.Log(gameManager.playerDied);
        if (!gameManager.playerDied)
        {
            player.AddForce(new Vector2(0, power * 1f));
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
        gameManager.playerDied = true;
        anim.SetTrigger("shock");
        gameManager.deathCount += 1;
        if(gameManager.deathCount >= 5 && !gameManager.instance.adsDeactivated)
        {
            Adsmanager.instance.GameOver();
        }
     //   player.transform.GetChild(0).SetParent(null);
    }

    public void playerEat()
    {
        gameManager.playerDied = true;
        gameManager.deathCount += 1;

        if (gameManager.deathCount >= 5 && !gameManager.instance.adsDeactivated)
        {
            Adsmanager.instance.GameOver();
        }
        //player.transform.GetChild(0).SetParent(null);

    }


    private void OnDrawGizmosSelected()
    {
        //Gizmos.DrawWireSphere(transform.position, 0.5f);
    }


    public void goBack()
    {
        SceneManager.LoadScene(2);
    }
}
