using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class targetScript : MonoBehaviour
{
    public GameObject target;
    public float duration = 5.0f;
    public float playTime = 30.0f;
    public  bool gunPickup = false;
    public bool gamestarted = false;
    private Vector3 pos;
    private Quaternion rot;
    // Start is called before the first frame update
    void Start()
    {
        //gunPickup = false;
        //StartCoroutine(Countdown());
        pos = gameObject.transform.position;
        rot = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if ((gunPickup == true) && (gamestarted == false))
        {
            gamestarted = true;
            StartCoroutine(Countdown());
            gunPickup = false;
        }
    }

    void spawnFirstTarget()
    {
        Instantiate(target, pos, rot);
    }

    void spawnTarget()
    {
        Instantiate(target, pos, rot);
        StartCoroutine(SpawnDelay());
    }

    private IEnumerator Countdown()
    {
        countdownTimer.startCountdownTimer(5f);
        yield return new WaitForSeconds(duration);
        spawnFirstTarget();
        StartCoroutine(SpawnDelay());
        countdownTimer.startCountdownTimer(30f);
        yield return new WaitForSeconds(playTime);
        StopAllCoroutines();
        gamestarted = false;
        Ticket.addTicketAmt(shootingGallery.shootScore);
        shootingGallery.shootScore = 0;
    }

    private IEnumerator SpawnDelay()
    {
        int randNum = Random.Range(1, 3);
        yield return new WaitForSeconds(randNum);
        spawnTarget();
    }
}
