using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class gun : MonoBehaviour
{
    public SteamVR_Action_Boolean gripAction;
    public SteamVR_Action_Boolean TriggerPress;
    public SteamVR_Action_Boolean dropGrip;
    private Vector3 originalPos;
    private bool handhere = false;
    private bool isHeld = false;
    public float timeBetweenBullets = 1.0f;
    public float bulletRange = 5.0f;
    public float timer;
    public GameObject rightHand;
    private GameObject currentBullet;
    public GameObject Bullet;
    public float bulletSpeed = 1f;
    public GameObject bulletSource;
    public GameObject topTarget;
    public GameObject botTarget;
    public GameObject pickupAudio;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        isHeld = false;
        handhere = false;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        print(isHeld);
        timer += Time.deltaTime;

        if (handhere && isHeld == false)
        {
            if (gripAction.GetStateDown(SteamVR_Input_Sources.RightHand) || gripAction.GetStateDown(SteamVR_Input_Sources.LeftHand))
            {
                pickupAudio.GetComponent<AudioSource>().enabled = true;
                StartCoroutine(pickupSound());
                isHeld = true;
                //var rotationVec = transform.rotation.eulerAngles;
                //rotationVec.x = 0;
                //rotationVec.y = 80;
                //rotationVec.z = 0;
                //transform.rotation = Quaternion.Euler(rotationVec);
                //transform.SetParent(rightHand.gameObject.transform, false);
                transform.rotation = rightHand.gameObject.transform.rotation;
                transform.position = rightHand.gameObject.transform.position; 
                transform.parent = rightHand.gameObject.transform;
                if ((topTarget.GetComponent<targetScript>().gamestarted == false) && (botTarget.GetComponent<targetScript>().gamestarted == false))
                {
                    topTarget.GetComponent<targetScript>().gunPickup = true;
                    botTarget.GetComponent<targetScript>().gunPickup = true;
                }
            }
            
        }
        /*if (isHeld)
        {
            var rotvec = rightHand.gameObject.transform.rotation.eulerAngles;
            //rotvec.x = rightHand.transform.rotation.x + 45;
            //rotvec.y = rightHand.transform.rotation.y - 90;
            //rotvec.z = rightHand.transform.rotation.z - 90;
            transform.rotation = Quaternion.Euler(rotvec);
        }*/

        if (isHeld) {
            if (timer > timeBetweenBullets && TriggerPress.GetStateDown(SteamVR_Input_Sources.RightHand))
            {
                shoot();
            }
            if (dropGrip.GetStateDown(SteamVR_Input_Sources.RightHand) || dropGrip.GetStateDown(SteamVR_Input_Sources.LeftHand))
            {
                transform.parent = null;
                isHeld = false;
                gameObject.transform.position = originalPos;
            }
        }
    }

    public void shoot()
    {
        print("me shoot");
        gameObject.GetComponent<AudioSource>().enabled = true;
        StartCoroutine(sound());
        Quaternion rot = Quaternion.Euler(bulletSource.transform.position.x, bulletSource.transform.position.y, bulletSource.transform.position.z);
        currentBullet = Instantiate(Bullet, bulletSource.transform.position, rot);
        //currentBullet.transform.Rotate(0, 90, 0);
        //currentBullet.transform.rotation = rot;
        currentBullet.GetComponent<Rigidbody>().AddForce(rightHand.transform.forward * bulletSpeed);
        Destroy(currentBullet, bulletRange);
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
                /*if (gripAction.GetStateDown(SteamVR_Input_Sources.RightHand) || gripAction.GetStateDown(SteamVR_Input_Sources.LeftHand))
                {
                    isHeld = true;
                    transform.parent = other.gameObject.transform;
                    transform.localPosition = other.gameObject.transform.position;
                }*/
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
