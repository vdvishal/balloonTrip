using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    public float speed;
    private Vector3 lasCamPos;
    public Transform spawnPoint;
    public Sprite backGround;

    // Start is called before the first frame update
    void Start()
    {
        lasCamPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.Translate(new Vector2(speed * Time.deltaTime, 0f));
      
    }
}
