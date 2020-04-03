using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class collectTicket : MonoBehaviour
{
    public SteamVR_Action_Boolean grabgrip;
    public bool playerHandHere;
    // Start is called before the first frame update
    void Start()
    {
        playerHandHere = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHandHere)
        {
            if (grabgrip.GetStateDown(SteamVR_Input_Sources.RightHand)){
                playerStats.addPlayerTickets(1);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerHand"))
        {
            print("Player hand enter ticket");
            playerHandHere = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerHand"))
        {
            playerHandHere = false;
        }
    }
}
