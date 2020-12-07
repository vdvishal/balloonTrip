using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class fish : MonoBehaviour
{
    float rand;
    public fishSpawner fishSpawner;

    public void eatPlayer()
    {
            this.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            rand = Random.Range(0, 10);


            if (rand < 12)
            {
                collision.GetComponent<CapsuleCollider2D>().enabled = false;
                collision.transform.SetParent(gameObject.transform);
                collision.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
 
                collision.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;

                collision.enabled = false;
            }else
            {
                // do nothing
            }


        }
    }

    public void enableSprite()
    {
        if (rand < 12)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            // do nothing
        }
        
    }

    public void disablePlayer()
    {
        if (rand < 12)
        {
            //transform.GetChild(1).transform.localPosition = new Vector3(-0.172f, -0.081f);

            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;


            transform.GetChild(1).transform.GetComponent<ballon_movement>().playerEat();
        }
        else
        {
            // do nothing
        }

    }

    public void gameOver()
    {
        if (rand < 12)
        {
            fishSpawner = FindObjectOfType<fishSpawner>();
            fishSpawner.spawned = false;
            Destroy(this.gameObject);
        }
        else
        {
            fishSpawner.spawned = false;
            Destroy(this.gameObject);
        }

    }
}
