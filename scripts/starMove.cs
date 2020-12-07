using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starMove : MonoBehaviour
{
    public float moveSpeed;
    float rand1;
    float rand2;
    private ballon_movement player;
    private audioManager audioMan;
    private bool hit = false;
    // Start is called before the first frame update

    // Start is called before the first frame update
    void Start()
    {
        audioMan = FindObjectOfType<audioManager>();
        rand1 = Random.Range(-1, 2);
          rand2 = Random.Range(-1, 2);

        GetComponent<Rigidbody2D>().velocity = (new Vector2(rand1*moveSpeed,rand2*moveSpeed)*Time.deltaTime);
        player = FindObjectOfType<ballon_movement>();// GetComponent<ballon_movement>();

    }

    private void Update()
    {
        rand1 = Random.Range(-1, 2);
        rand2 = Random.Range(-1, 2);

        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, 0.3f);

        foreach (var item in col)
        {
            if (item.CompareTag("Respawn"))
            {
                GetComponent<Rigidbody2D>().velocity = (new Vector2(rand1 * moveSpeed, rand2 * moveSpeed) * Time.deltaTime);
            }

            if (item.CompareTag("Player") && !hit)
            {
                hit = true;
                player.playerHit();
                audioMan.Play("electrocute");
            }
        }
    }

 
    // Update is called once per frame

}
