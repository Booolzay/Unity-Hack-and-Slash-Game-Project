using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

//Used this Script In a seperate Cylinder Collider for the player object instead of adding it directly to the player object is because if added directly to the player object it causes problem with detection when enemy hits the shield, causing player to take damage through the shield

public class PlayerHealth : MonoBehaviour
{
    
    public Slider playerHealth;
    public AudioSource hitAudio;

    public GameObject loseMassege;
    public GameObject winMassege;
    public GameObject instructions;
    public string menuScreen;
    

    void Start()
    {
        playerHealth.value = 200;
        loseMassege.SetActive(false);
        winMassege.SetActive(false);
        instructions.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("WaitForInstructions");
        if(playerHealth.value <= 0)
        {
            Debug.Log("player dead");
            loseMassege.SetActive(true);
            StartCoroutine("WaitForSec");
        }
        if(Score.defaultScore >= 800)
        {
            Debug.Log("player Win");
            winMassege.SetActive(true);
            StartCoroutine("WaitForSec");
        }
    }

    private void OnTriggerEnter(Collider player)
    {
        if(player.gameObject.tag == "enemy")
        {
            playerHealth.value -= 25;
            hitAudio.Play();
        } 
        
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(3);
        Destroy(loseMassege);
        SceneManager.LoadScene(menuScreen);
        Cursor.lockState = CursorLockMode.None;

        yield return new WaitForSeconds(5);
        instructions.SetActive(false);
    }
    IEnumerator WaitForInstructions()
    {
        
        yield return new WaitForSeconds(5);
        Destroy(instructions);
    }
}
