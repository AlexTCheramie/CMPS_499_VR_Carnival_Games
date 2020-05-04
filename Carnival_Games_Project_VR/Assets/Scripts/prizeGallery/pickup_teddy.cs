using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using TMPro;

public class pickup_teddy : MonoBehaviour
{
    public SteamVR_Action_Boolean gripAction;
    public SteamVR_Action_Boolean TriggerPress;
    public SteamVR_Action_Boolean dropGrip;
    private Vector3 originalPos;
    private Quaternion originalRot;
    private bool isPurchased = false;
    private bool handhere = false;
    private bool isHeld = false;
    private bool heldRight = false;
    private bool heldLeft = false;
    public GameObject rightHand;
    public GameObject lefthand;
    public float cost = 10;
    public GameObject pickupAudio;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        originalRot = gameObject.transform.rotation;
        isHeld = false;
        handhere = false;
        isPurchased = false;
        heldRight = false;
        heldLeft = false;
}

    // Update is called once per frame
    void Update()
    {
        if (isPurchased)
        {
            if (handhere && isHeld == false)
            {
                if (gripAction.GetStateDown(SteamVR_Input_Sources.RightHand))
                {
                    pickupAudio.GetComponent<AudioSource>().enabled = true;
                    StartCoroutine(pickupSound());
                    isHeld = true;
                    heldRight = true;
                    heldLeft = false;
                    transform.SetParent(rightHand.gameObject.transform, false);
                    transform.rotation = rightHand.gameObject.transform.rotation;
                    transform.position = rightHand.gameObject.transform.position;
                }
                if (gripAction.GetStateDown(SteamVR_Input_Sources.LeftHand))
                {
                    pickupAudio.GetComponent<AudioSource>().enabled = true;
                    StartCoroutine(pickupSound());
                    isHeld = true;
                    heldRight = false;
                    heldLeft = true;
                    transform.SetParent(lefthand.gameObject.transform, false);
                    transform.rotation = lefthand.gameObject.transform.rotation;
                    transform.position = lefthand.gameObject.transform.position;
                }
            }

            if (heldRight)
            {
                if (TriggerPress.GetStateDown(SteamVR_Input_Sources.RightHand))
                {
                    gameObject.GetComponent<AudioSource>().enabled = true;
                    StartCoroutine(sound());
                }
                if (dropGrip.GetStateDown(SteamVR_Input_Sources.RightHand))
                {
                    transform.parent = null;
                    isHeld = false;
                    heldRight = false;
                    gameObject.transform.position = originalPos;
                }
            }
            if (heldLeft)
            {
                if (TriggerPress.GetStateDown(SteamVR_Input_Sources.LeftHand))
                {
                    gameObject.GetComponent<AudioSource>().enabled = true;
                    StartCoroutine(sound());
                }
                if (dropGrip.GetStateDown(SteamVR_Input_Sources.LeftHand))
                {
                    transform.parent = null;
                    isHeld = false;
                    heldLeft = false;
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
                    if (gripAction.GetStateDown(SteamVR_Input_Sources.RightHand))
                    {
                        pickupAudio.GetComponent<AudioSource>().enabled = true;
                        StartCoroutine(pickupSound());
                        playerStats.addPlayerTickets(0 - cost);
                        transform.Find("price_teddy").GetComponent<MeshRenderer>().enabled = false;
                        isHeld = true;
                        heldRight = true;
                        heldLeft = false;
                        isPurchased = true;
                        transform.SetParent(rightHand.gameObject.transform, false);
                        transform.rotation = rightHand.gameObject.transform.rotation;
                        transform.position = rightHand.gameObject.transform.position;
                    }
                    if (gripAction.GetStateDown(SteamVR_Input_Sources.LeftHand))
                    {
                        pickupAudio.GetComponent<AudioSource>().enabled = true;
                        StartCoroutine(pickupSound());
                        playerStats.addPlayerTickets(0 - cost);
                        transform.Find("price_teddy").GetComponent<MeshRenderer>().enabled = false;
                        isHeld = true;
                        heldRight = false;
                        heldLeft = true;
                        isPurchased = true;
                        transform.SetParent(lefthand.gameObject.transform, false);
                        transform.rotation = lefthand.gameObject.transform.rotation;
                        transform.position = lefthand.gameObject.transform.position;
                    }
                }
            }
            if (heldRight)
            {
                if (TriggerPress.GetStateDown(SteamVR_Input_Sources.RightHand))
                {
                    gameObject.GetComponent<AudioSource>().enabled = true;
                    StartCoroutine(sound());
                }
                if (dropGrip.GetStateDown(SteamVR_Input_Sources.RightHand))
                {
                    transform.parent = null;
                    isHeld = false;
                    heldRight = false;
                    gameObject.transform.position = originalPos;
                }
            }
            if (heldLeft)
            {
                if (TriggerPress.GetStateDown(SteamVR_Input_Sources.LeftHand))
                {
                    gameObject.GetComponent<AudioSource>().enabled = true;
                    StartCoroutine(sound());
                }
                if (dropGrip.GetStateDown(SteamVR_Input_Sources.LeftHand))
                {
                    transform.parent = null;
                    isHeld = false;
                    heldLeft = false;
                    gameObject.transform.position = originalPos;
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

    IEnumerator sound()
    {
        yield return new WaitForSeconds(gameObject.GetComponent<AudioSource>().clip.length);
        gameObject.GetComponent<AudioSource>().enabled = false;
    }

    IEnumerator pickupSound()
    {
        yield return new WaitForSeconds(pickupAudio.GetComponent<AudioSource>().clip.length);
        pickupAudio.GetComponent<AudioSource>().enabled = false;
    }
}
