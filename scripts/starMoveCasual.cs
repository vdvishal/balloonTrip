using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starMoveCasual : MonoBehaviour
{
    public float dirc;
    private levelManager player;
    private audioManager audioMan;
    private bool hit = false;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        audioMan = FindObjectOfType<audioManager>();
 
        GetComponent<Rigidbody2D>().velocity = (new Vector2(dirc * moveSpeed, 0 * moveSpeed) * Time.deltaTime);
        player = FindObjectOfType<levelManager>();// GetComponent<ballon_movement>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, 0.3f);

        foreach (var item in col)
        {

            if (item.CompareTag("Player") && !hit)
            {
                hit = true;
                player.playerHit();
                audioMan.Play("electrocute");
            }

            if (item.CompareTag("Respawn"))
            {
                Destroy(gameObject);
            }

        }
    }
}
