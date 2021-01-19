using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Adapted from 2 tutorials : grabbing: https://www.youtube.com/watch?v=bA12WEA5MLo , thorwing: https://www.youtube.com/watch?v=_xMhkK6GTXA

public class throwObj : MonoBehaviour
{
    RaycastHit grabRay;
    GameObject grabAble;
    public Transform grabPos;

    public float force = 200;
    public GameObject throwDirection;

    public static bool magicState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("e") && Physics.Raycast(transform.position, transform.forward, out grabRay, 50) && grabRay.transform.GetComponent<Rigidbody>())
        {
            grabAble = grabRay.transform.gameObject;
            magicState = true;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            grabAble.GetComponent<Rigidbody>().AddForce(throwDirection.transform.forward*force) ;
            grabAble = null;
            magicState = false;
        }

        if (grabAble)
        {
            grabAble.GetComponent<Rigidbody>().velocity = (grabPos.position - grabAble.transform.position) * 50;
        }
    }

    
}
