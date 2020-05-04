using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using TMPro;

public class pickup_spray : MonoBehaviour
{
    public SteamVR_Action_Boolean gripAction;
    public SteamVR_Action_Boolean TriggerPress;
    public SteamVR_Action_Boolean dropGrip;
    private Vector3 originalPos;
    private Quaternion originalRot;
    private bool isPurchased = false;
    private bool handhere = false;
    private bool isHeld = false;
    public GameObject rightHand;
    public float cost = 20;
    public GameObject pickupAudio;


    public float sprayDecay = 20;
    //public float timeBetweenSprayObjects = 1;
    //public float timer;
    public bool spray = false;
    public GameObject sprayObject;
    public GameObject spraySource;
    private GameObject currentSpray;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        originalRot = gameObject.transform.rotation;
        isHeld = false;
        handhere = false;
        isPurchased = false;

        //timer = 0;
        spray = false;
    }

    // Update is called once per frame
    void Update()
    {
        //timer += Time.deltaTime;
        if (spray)
        {
            gameObject.GetComponent<AudioSource>().enabled = true;
            startSpray();

            if (TriggerPress.GetLastStateUp(SteamVR_Input_Sources.RightHand))
            {
                gameObject.GetComponent<AudioSource>().enabled = false;
                spray = false;
            }
        }

        if (isPurchased)
        {
            if (handhere && isHeld == false)
            {
                if (gripAction.GetStateDown(SteamVR_Input_Sources.RightHand) || gripAction.GetStateDown(SteamVR_Input_Sources.LeftHand))
                {
                    pickupAudio.GetComponent<AudioSource>().enabled = true;
                    StartCoroutine(pickupSound());
                    isHeld = true;
                    transform.SetParent(rightHand.gameObject.transform, false);
                    transform.rotation = rightHand.gameObject.transform.rotation;
                    transform.position = rightHand.gameObject.transform.position;
                }
            }

            if (isHeld)
            {
                if (TriggerPress.GetStateDown(SteamVR_Input_Sources.RightHand))
                {
                    spray = true;
                }

                if (dropGrip.GetStateDown(SteamVR_Input_Sources.RightHand) || dropGrip.GetStateDown(SteamVR_Input_Sources.LeftHand))
                {
                    transform.parent = null;
                    isHeld = false;
                    gameObject.transform.position = originalPos;
                }
            }
        }
        if (!isPurchased)
        {
            if (handhere && isHeld == false)
            {
                if (playerStats.playerTickets >= cost)
                {
                    if (gripAction.GetStateDown(SteamVR_Input_Sources.RightHand) || gripAction.GetStateDown(SteamVR_Input_Sources.LeftHand))
                    {
                        pickupAudio.GetComponent<AudioSource>().enabled = true;
                        StartCoroutine(pickupSound());
                        playerStats.addPlayerTickets(0 - cost);
                        transform.Find("price_spray").GetComponent<MeshRenderer>().enabled = false;
                        isHeld = true;
                        isPurchased = true;
                        transform.SetParent(rightHand.gameObject.transform, false);
                        transform.rotation = rightHand.gameObject.transform.rotation;
                        transform.position = rightHand.gameObject.transform.position;
                    }
                }
            }
            if (isHeld)
            {
                if (TriggerPress.GetStateDown(SteamVR_Input_Sources.RightHand))
                {
                    spray = true;
                }

                if (dropGrip.GetStateDown(SteamVR_Input_Sources.RightHand) || dropGrip.GetStateDown(SteamVR_Input_Sources.LeftHand))
                {
                    transform.parent = null;
                    isHeld = false;
                    gameObject.transform.position = originalPos;
                    gameObject.transform.rotation = originalRot;
                }
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        print("SOMETHINGS IN MY TRIGGER RANGE");
        if (isHeld == false)
        {
            if (other.gameObject.CompareTag("PlayerHand"))
            {
                handhere = true;
                print("PLAYER HAND FOUND!!!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        handhere = false;
    }

    public void startSpray()
    {
        //Quaternion rot = Quaternion.Euler(bulletSource.transform.position.x, bulletSource.transform.position.y-90, bulletSource.transform.position.z);
        currentSpray = Instantiate(sprayObject, spraySource.transform.position, spraySource.transform.rotation);
        Destroy(currentSpray, sprayDecay);
    }

    IEnumerator pickupSound()
    {
        yield return new WaitForSeconds(pickupAudio.GetComponent<AudioSource>().clip.length);
        pickupAudio.GetComponent<AudioSource>().enabled = false;
    }
}
