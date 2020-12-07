﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ballon_movement : MonoBehaviour
{

    public Rigidbody2D player;

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

        if (gameManager.playerDied)
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
        if (!gameManager.playerDied)
        {
            player.AddForce(new Vector2(0, power * 1f * Time.deltaTime));
            anim.SetTrigger("boost");

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
     //   player.transform.GetChild(0).SetParent(null);
    }

    public void playerEat()
    {
        gameManager.playerDied = true;
        //player.transform.GetChild(0).SetParent(null);
 
    }

    private void OnDrawGizmosSelected()
    {
        //Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}