using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishSpawner : MonoBehaviour
{
    public GameObject fish;
    float rand;
    public bool spawned;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        rand = Random.Range(0, 20);

        if (collision.transform.CompareTag("Player") && rand < 10)
        {
            if (!spawned)
            {
                Instantiate(fish, collision.transform.position + new Vector3(0,-1.5f,0),Quaternion.identity);
                spawned = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.transform.CompareTag("Player") && rand < 10)
        {
            if (!spawned)
            {
                Instantiate(fish, collision.transform.position + new Vector3(0, -1.5f, 0), Quaternion.identity);
                spawned = true;
            }
        }

    }

}
