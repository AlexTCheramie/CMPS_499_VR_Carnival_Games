﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetMove : MonoBehaviour
{
    public float targetSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += targetSpeed * Time.deltaTime;
        transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("targetKillZone"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            shootingGallery.addTargetScore(1);
        }
    }
}
