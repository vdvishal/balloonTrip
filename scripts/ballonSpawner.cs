using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballonSpawner : MonoBehaviour
{
    public GameObject ballon;
    float lastTime;
    float spawnTime = 10f;

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastTime > spawnTime)
        {
            if (gameManager.instance != null && gameManager.instance.unlocked.Length > 0)
            {
                int rand = Random.Range(0, gameManager.instance.unlocked.Length);
 
                GameObject starPre = Instantiate(gameManager.instance.unlocked[rand], transform);
                starPre.transform.SetParent(null);
                starPre.transform.position = new Vector3(starPre.transform.position.x + Random.Range(-2, 2), starPre.transform.position.y + Random.Range(-1f, 0), 0f);
                lastTime = Time.time;
            }
            else
            {
                GameObject starPre = Instantiate(ballon, transform);
                starPre.transform.SetParent(null);
                starPre.transform.position = new Vector3(starPre.transform.position.x + Random.Range(-2, 2), starPre.transform.position.y + Random.Range(-1f, 0), 0f);
                lastTime = Time.time;
            }



        }
    }
}
