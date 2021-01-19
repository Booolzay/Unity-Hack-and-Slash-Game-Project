using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;


    void Start()
    {
        anim.SetFloat("vertical", Input.GetAxis("Vertical"));
        anim.SetFloat("horizontal", Input.GetAxis("Horizontal"));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("w"))
        {
            anim.SetFloat("vertical", 0);
        }
        else if (Input.GetKeyUp("w")){
            anim.SetFloat("vertical", 1);
        }
    }
}
