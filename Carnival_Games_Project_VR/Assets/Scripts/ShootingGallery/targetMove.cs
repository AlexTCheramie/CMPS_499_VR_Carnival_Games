using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetMove : MonoBehaviour
{
    public float targetSpeed = 2.0f;
    //public AudioSource destroyed;
    // Start is called before the first frame update
    void Start()
    {
        //destroyed = GetComponent<AudioSource>();
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
        //if (other.gameObject.CompareTag("bullet"))
        //{
        //    StartCoroutine(Destroy(destroyed.clip.length));
        //}
    }

    //IEnumerator Destroy(float waitTime)
    //{
     //   destroyed.Play();
     //   yield return new WaitForSeconds(waitTime);
      //  Destroy(gameObject);
        //INCREASE SCORE!!!!!
    //}
}
