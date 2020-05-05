using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class pickupObject : MonoBehaviour
{
    public SteamVR_Action_Boolean gripAction;
    //public SteamVR_Action_Boolean TriggerPress;
    public SteamVR_Action_Boolean dropGrip;
    private Vector3 originalPos;
    private bool isPurchased = false;
    private bool handhere = false;
    private bool isHeld = false;
    public GameObject rightHand;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        isHeld = false;
        handhere = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (handhere && isHeld == false)
        {
            if (gripAction.GetStateDown(SteamVR_Input_Sources.RightHand) || gripAction.GetStateDown(SteamVR_Input_Sources.LeftHand))
            {
                isHeld = true;
                transform.rotation = rightHand.gameObject.transform.rotation;
                transform.position = rightHand.gameObject.transform.position;
                transform.parent = rightHand.gameObject.transform;
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
