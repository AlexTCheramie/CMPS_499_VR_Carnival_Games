using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using TMPro;

public class pickup_teddy : MonoBehaviour
{
    public SteamVR_Action_Boolean gripAction;
    //public SteamVR_Action_Boolean TriggerPress;
    public SteamVR_Action_Boolean dropGrip;
    private Vector3 originalPos;
    private Quaternion originalRot;
    private bool isPurchased = false;
    private bool handhere = false;
    private bool isHeld = false;
    public GameObject rightHand;
    public float cost = 10;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        originalRot = gameObject.transform.rotation;
        isHeld = false;
        handhere = false;
        isPurchased = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPurchased)
        {
            if (handhere && isHeld == false)
            {
                if (gripAction.GetStateDown(SteamVR_Input_Sources.RightHand) || gripAction.GetStateDown(SteamVR_Input_Sources.LeftHand))
                {
                    isHeld = true;
                    transform.SetParent(rightHand.gameObject.transform, false);
                    transform.rotation = rightHand.gameObject.transform.rotation;
                    transform.position = rightHand.gameObject.transform.position;
                }
            }

            if (isHeld)
            {
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
                        playerStats.addPlayerTickets(0 - cost);
                        transform.Find("price_teddy").GetComponent<MeshRenderer>().enabled = false;
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

}
