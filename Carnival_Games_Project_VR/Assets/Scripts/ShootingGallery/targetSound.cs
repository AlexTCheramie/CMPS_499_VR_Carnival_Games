using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetSound : MonoBehaviour
{
    static bool playsound = false;
    public GameObject soundtoplay;

    // Start is called before the first frame update
    void Start()
    {
        playsound = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playsound)
        {
            print("playsound is true");
            playsound = false;
            StartCoroutine(BreakTarget());
        }
    }

    public static void playSound()
    {
        print("playsound got called");
        playsound = true;
    }

    IEnumerator BreakTarget()
    {
        print("im playing the sound");
        soundtoplay.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(soundtoplay.GetComponent<AudioSource>().clip.length);
    }
}
