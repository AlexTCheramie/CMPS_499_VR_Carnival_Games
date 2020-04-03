using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveMent : MonoBehaviour
{
    public float playerSpeed = 5.0f;
    public GameObject topTargetSpawn;
    public GameObject botTargetSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("w"))
        {
            pos.z += playerSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            pos.z -= playerSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.x += playerSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            pos.x -= playerSpeed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            topTargetSpawn.GetComponent<targetScript>().StartTargets();
            botTargetSpawn.GetComponent<targetScript>().StartTargets();
        }

        transform.position = pos;
    }
}
