using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class shootingGallery : MonoBehaviour
{
    public static float shootScore;
    public static float timeRemaining;
    public TextMeshPro score;
    // Start is called before the first frame update
    void Start()
    {
        shootScore = 0f;
        timeRemaining = 30f;
    }

    // Update is called once per frame
    void Update()
    {
        score.SetText("Score: " + shootScore.ToString()); 
    }

    public static void addTargetScore(float amt)
    {
        shootScore += amt;
    }

}
