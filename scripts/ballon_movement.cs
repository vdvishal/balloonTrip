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

 

 
    //private bool death;

    float move_x;



    // Start is called before the first frame update
    void Start()
    {
 
        player.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = gameManager.instance.selectedBalloon;

    }

    // Update is called once per frame
    void Update()
    {
 

    }


    private void OnDrawGizmosSelected()
    {
        //Gizmos.DrawWireSphere(transform.position, 0.5f);
    }


    public void goBack()
    {
        SceneManager.LoadScene(2);
    }

    public void playerEat()
    {

        gameManager.instance.playerDied = true;
        if (gameManager.instance.playerDied && !gameManager.instance.continueGame)
        {
            gameManager.instance.deathCount += 1;

            if (gameManager.instance.deathCount >= 5 && !gameManager.instance.adsDeactivated)
            {
                Adsmanager.instance.GameOver();
            }
        }



        //player.transform.GetChild(0).SetParent(null);

    }

}
