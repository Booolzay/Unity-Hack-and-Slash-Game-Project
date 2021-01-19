using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code Ref: https://www.firemind-academy.com/p/create-healthbar-in-3d-space-unity/

public class PlayerViewLock : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera player;

    void Start()
    {
       

    }

    // Update is called once per frame
    void Update() //Rotates the enemy healthBar based on and to face the Player
    {
        Vector3 lookDir = player.transform.position - transform.position;
        lookDir.x = lookDir.z = 0.0f;
        transform.LookAt(player.transform.position - lookDir);
        transform.Rotate(0, 0, 0); //Look Angle
    }
}
