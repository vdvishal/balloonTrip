using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backGround : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject back;

    private void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.transform.CompareTag("MainCamera"))
        {
            transform.SetPositionAndRotation(new Vector3(spawnPoint.position.x, spawnPoint.position.y,0), Quaternion.identity);
        }
    }
}
