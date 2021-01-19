using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUp : MonoBehaviour
{

    public GameObject picked;
    public GameObject pick;

    public GameObject pickedKatana;
    public GameObject pickKatana;

    public GameObject pickedDarkSword;
    public GameObject pickDarkSword;

    public GameObject pickedModernSword;
    public GameObject pickModernSword;

    public GameObject shieldPickUp;
    public GameObject shieldPicked;
    public bool isEquipped;
    // Start is called before the first frame update
    void Start()//Primarily deacivating all puickupable and usable objects
    {
        picked.SetActive(false);
        pickedKatana.SetActive(false);
        pickedDarkSword.SetActive(false);
        pickedModernSword.SetActive(false);
        shieldPicked.SetActive(false);
    }
    private void OnTriggerEnter(Collider weaponCollider)
    {
        if (isEquipped || !isEquipped) //Calling these conditions while in or not in Equipped state so that, player can pick another weapon while player is equipped or is not
        {
            if (weaponCollider.gameObject.tag == "pickupObj") //Picks up the sword Tagged "pickupObj" and deactivates the rests ---> similar goes for The next weapons
            {
                picked.SetActive(true); // makes picked up weapon visible on hand
                pick.SetActive(false);// makes picked up weapon invisible from Pickup location

                pickedKatana.SetActive(false);
                pickedDarkSword.SetActive(false);
                pickedModernSword.SetActive(false);

                isEquipped = true;
            }
            else if (weaponCollider.gameObject.tag == "katana")
            {
                pickedKatana.SetActive(true);
                pickKatana.SetActive(false);

                picked.SetActive(false);
                pickedDarkSword.SetActive(false);
                pickedModernSword.SetActive(false);

                isEquipped = true;
            }
            else if (weaponCollider.gameObject.tag == "darkSword")
            {
                pickedDarkSword.SetActive(true);
                pickDarkSword.SetActive(false);

                picked.SetActive(false);
                pickedKatana.SetActive(false);
                pickedModernSword.SetActive(false);

                isEquipped = true;
            }
            else if (weaponCollider.gameObject.tag == "bigSword")
            {
                pickedModernSword.SetActive(true);
                pickModernSword.SetActive(false);

                picked.SetActive(false);
                pickedKatana.SetActive(false);
                pickedDarkSword.SetActive(false);

                isEquipped = true;
            }

            if(weaponCollider.gameObject.tag == "shield")
            {
                shieldPicked.SetActive(true);
                shieldPickUp.SetActive(false);
                isEquipped = true;
            }
        }
    }


}
