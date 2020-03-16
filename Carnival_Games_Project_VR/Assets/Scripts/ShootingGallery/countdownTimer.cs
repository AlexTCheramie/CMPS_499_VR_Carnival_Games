using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class countdownTimer : MonoBehaviour
{
    static public float currentTime = 0f;
    static public float startingTime = 0f;
    public TextMeshPro timer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= 1 * Time.deltaTime;
            //timer.text = currentTime.ToString("0f");
            timer.SetText("Time: " + currentTime.ToString("0"));
        }

    }

    static public void startCountdownTimer(float amount)
    {
        startingTime = amount;
        currentTime = amount;
    }
}
