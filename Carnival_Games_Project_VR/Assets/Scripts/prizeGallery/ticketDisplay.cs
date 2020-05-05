using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ticketDisplay : MonoBehaviour
{
    public TextMeshPro tickets;
    // Start is called before the first frame update
    void Start()
    {
        tickets.SetText("Your Tickets: " + playerStats.playerTickets);
    }

    // Update is called once per frame
    void Update()
    {
        tickets.SetText("Your Tickets: " + playerStats.playerTickets);
    }
}
