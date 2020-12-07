using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balloonPop : MonoBehaviour
{
    public audioManager audioMan;
    public gameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        audioMan = FindObjectOfType<audioManager>();
        gameManager = FindObjectOfType<gameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("BalloonPop");
            audioMan.Play("BalloonPop");
            gameManager.updateScore();
            Destroy(this.gameObject);
        }
    }
}
