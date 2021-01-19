using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthPicked : MonoBehaviour
{
    public Slider healthBar;

    private void OnTriggerEnter(Collider health)
    {
        if(health.gameObject.tag == "Player") //Disappears when picked
        {
            this.gameObject.SetActive(false);
            healthBar.value += 50;
            Debug.Log("HPUP");
        }
    }
}
