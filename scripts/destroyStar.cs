using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyStar : MonoBehaviour
{
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
        
        if (collision.CompareTag("star"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("platform"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("ballon"))
        {
            Destroy(collision.gameObject);
        }
    }
}
