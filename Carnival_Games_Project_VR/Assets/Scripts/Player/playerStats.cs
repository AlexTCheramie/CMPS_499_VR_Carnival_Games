﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : MonoBehaviour
{
    public static float playerTickets;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print("player has " + playerTickets + " tickets");
    }

    public static void addPlayerTickets(float amt)
    {
        playerTickets += amt;
    }
}
