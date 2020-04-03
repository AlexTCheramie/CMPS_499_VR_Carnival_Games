using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ticket : MonoBehaviour
{
    public GameObject ticketObj;
    public GameObject ticketSrc;
    public static float ticketAmt = 0;
    // Start is called before the first frame update
    void Start()
    {
        ticketAmt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (ticketAmt > 0)
        {
            Instantiate(ticketObj, ticketSrc.transform.position, ticketSrc.transform.rotation);
            ticketAmt--;
        }
    }

    public static void addTicketAmt(float amt)
    {
        ticketAmt += amt;
    }

}
