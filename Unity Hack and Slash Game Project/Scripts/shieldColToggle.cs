using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldColToggle : MonoBehaviour
{
    // Start is called before the first frame update
    public Collider cols;
    void Start()
    {
        cols = GetComponent<Collider>(); //gets the collider component of this GameObject
    }

    // Update is called once per frame

    void Update() //updates everyframe so that the collider toggles and stays that way every frame Rigth mouse button is pressed
    {
        if (Input.GetMouseButtonDown(1))
        {
            cols.enabled = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            cols.enabled = false;
        }
    }
}
