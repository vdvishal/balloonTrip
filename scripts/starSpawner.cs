using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject star;
    public GameObject star1;
    public GameObject star2;

    float lastTime;
    float lastTime2;
    float startTime;

    float spawnTime = 2.85f;
    // Update is called once per frame

    private void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if(Time.time - lastTime > spawnTime && Time.time - startTime > 6)
        {
            GameObject starPre = Instantiate(star,transform);
            starPre.transform.SetParent(null);
            starPre.transform.position = new Vector3(starPre.transform.position.x, starPre.transform.position.y, 0f);
            lastTime = Time.time;
        }

        if (Time.time - lastTime2 > 6 && star2 != null && star1 != null)
        {
            int rand = Random.Range(0,12);
            int rand2 = Random.Range(-2, 2);
            int rand3 = Random.Range(-2, 2);

            if (rand < 5)
            {
                GameObject starPre = Instantiate(star1, transform);
                starPre.transform.SetParent(null);
                starPre.transform.position = new Vector3(starPre.transform.position.x + rand2, starPre.transform.position.y + rand2, 0f);
                lastTime2 = Time.time;
            }
            else if (rand > 7)
            {
                GameObject starPre = Instantiate(star2, transform);
                starPre.transform.SetParent(null);
                starPre.transform.position = new Vector3(starPre.transform.position.x + rand3, starPre.transform.position.y + rand3, 0f);
                lastTime2 = Time.time;
            }

        }

    }
}
