using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balloonSpawnerCasual : MonoBehaviour
{

    float time = 1.5f;
    float time1 = 1.5f;

    float time2 = 4f;

    float SpawnTime;

    public GameObject example;

    public GameObject starObject;

    public GameObject[] spawner;

    public GameObject[] leftSpawner;

    public GameObject[] rightSpawnerStars;

    public float starSpawnTime1;
    public float starSpawnTime2;

    bool spawnedStar;

    // Start is called before the first frame update
    void Start()
    {
        SpawnTime = Time.time;
        starSpawnTime1 = Time.time;
        starSpawnTime2 = Time.time;

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - SpawnTime > time)
        {
            spawnBalloons();
            
        }

        if (Time.time - starSpawnTime1 > time1 )
        {
            spawnStarsLeft();
           
        }

        if (Time.time - starSpawnTime2 > time2)
        {
            spawnStarsRight();
        }
    }

    void spawnBalloons()
    {
        
       int randspawner = Random.Range(0, spawner.Length);
        
        if (gameManager.instance != null && gameManager.instance.unlocked.Length > 0)
        {
            int rand = Random.Range(0, gameManager.instance.unlocked.Length);

            GameObject balloon = Instantiate(gameManager.instance.unlocked[rand], new Vector3(spawner[randspawner].transform.position.x, spawner[randspawner].transform.position.y), Quaternion.identity);

       balloon.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
         balloon.GetComponent<Rigidbody2D>().gravityScale = -0.04f;
        SpawnTime = Time.time;

        }
        else
        {
            GameObject balloon = Instantiate(example, new Vector3(spawner[randspawner].transform.position.x, spawner[randspawner].transform.position.y), Quaternion.identity);

            balloon.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            balloon.GetComponent<Rigidbody2D>().gravityScale = -0.04f;
            SpawnTime = Time.time;
        }


    }


    void spawnStarsLeft()
    {

        //   int rand = Random.Range(0, gameManager.instance.unlocked.Length);
        int randspawner = Random.Range(0, leftSpawner.Length);

        Debug.Log(leftSpawner.Length);

        GameObject star = Instantiate(starObject, new Vector3(leftSpawner[randspawner].transform.position.x, leftSpawner[randspawner].transform.position.y), Quaternion.identity);

        star.GetComponent<starMoveCasual>().dirc = 1;

        starSpawnTime1 = Time.time;


    }

    void spawnStarsRight()
    {

        //   int rand = Random.Range(0, gameManager.instance.unlocked.Length);
        int randspawner = Random.Range(0, rightSpawnerStars.Length);

        GameObject star = Instantiate(starObject, new Vector3(rightSpawnerStars[randspawner].transform.position.x, rightSpawnerStars[randspawner].transform.position.y), Quaternion.identity);

        star.GetComponent<starMoveCasual>().dirc = -1;

        starSpawnTime2 = Time.time;
        

    }
}
