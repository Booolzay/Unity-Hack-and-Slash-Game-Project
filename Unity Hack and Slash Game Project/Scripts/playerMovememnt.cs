using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//melee Combo technique ref : https://www.grimoirehex.com/unity-3d-combo-animation/

public class playerMovememnt : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterController controller;
    public float speed = 1f;
    public float runningSpeed = 1.5f;
    Vector3 velocity;
    public float gravity = -9.81f;

    public float jumpHeight = 10;
    public Transform groundCheck;
    public float groundDistacne = 0.4f;
    public LayerMask groundMask;

    //ground check;
    bool isGrounded;

    //EquipmentCheck
    public bool isEquipped;


    //Combat
    public int noOfClicks = 0;
    public bool canClick;
    Animator anim;
    public bool isBlocking;

    //Combat SFX
    public AudioSource combatSFX;
    public AudioClip atk1;
    public AudioClip atk2;
    public AudioClip atk3;

    //Audio 
    public AudioClip footStep;
    public AudioSource playerSFX;
    public AudioClip running;

    public AudioSource bgm;

    
    

    

    void Start()
    {
        anim = GetComponent<Animator>();
        noOfClicks = 0;
        canClick = true;
        isBlocking = false;
    }

    // Update is called once per frame
    void Update()
    {

        
        
        ControllerMovements();
        UnEquippedMovements();
        EquipedMovements();
        if (Input.GetMouseButtonDown(0))
        {
            ComboStarter();
        }

        magicStateMoves();
        playerAudioSFX();

        bgm.Play();


    }

    private void playerAudioSFX()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            playerSFX.clip = footStep;
            if (!playerSFX.isPlaying)
            {
                playerSFX.Play();
            }
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            playerSFX.clip = footStep;
            if (playerSFX.isPlaying)
            {
                playerSFX.Stop();
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerSFX.clip = running;
            if (!playerSFX.isPlaying)
            {
                playerSFX.Play();
            }
        }else if(Input.GetKeyUp(KeyCode.LeftShift)){
            playerSFX.clip = running;
            if (playerSFX.isPlaying)
            {
                playerSFX.Stop();
            }
        }





    }


    private void ControllerMovements()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistacne, groundMask);//Checks if the groundCheck object is touching the object masked with "gorund" Layer
        if (isGrounded && velocity.y < 0) //Stops incrementing velocity if player is on gorund
        {
            velocity.y = -2f;
            Debug.Log("isGornded");
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        //Vector3 run = (transform.right * x) + (transform.forward * z);
        controller.Move(move * speed * Time.deltaTime);
        

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && isGrounded)
        {
            controller.Move(move * runningSpeed * Time.deltaTime);
            Debug.Log("Forrest is Running");
        }
        else if (isGrounded)
        {
            controller.Move(move * speed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);

        }

        if (Input.GetMouseButtonDown(1)) // Added blocking animations in update fucntion instead of a specific state so that is works in all states
        {
            anim.SetBool("blooock", true);
        }
        if (Input.GetMouseButtonUp(1))
        {
            anim.SetBool("blooock", false);
        }
        


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

       
        
    }

    private void OnTriggerEnter(Collider Player)
    {
        if(Player.gameObject.tag == "pickupObj" || Player.gameObject.tag == "katana" || Player.gameObject.tag == "darkSword" || Player.gameObject.tag == "bigSword")
        {
            isEquipped = true;
            anim.SetInteger("equipe", 1);
            Debug.Log("collider is working");
            
        }
    }

    private void UnEquippedMovements()
    {
        //Jumps
        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.W) && isGrounded)
        {
            anim.SetInteger("jumpTrig", 1);
        }
        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            anim.SetInteger("jumpTrig", 1);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            anim.SetInteger("jumpTrig", 1);
        }
        else
        {
            anim.SetInteger("jumpTrig", 2);
        }


        //Vertical  movement animation triggers
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetInteger("vertMove", 1);
            
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            anim.SetInteger("vertMove", 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            anim.SetInteger("vertMove", 2);
        }

        //Sprint Animation Triggeres
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && isGrounded)
        {
            anim.SetInteger("runVert", 1);
        }
        else
        {
            anim.SetInteger("runVert", 0);
        }

        //Horizontal  movement animation triggers
        if (Input.GetKey(KeyCode.A))
        {
            anim.SetInteger("horizonMove", 1);
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("horizonMove", 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            anim.SetInteger("horizonMove", 2);
        }
    }

    private void EquipedMovements()
    {
        if (isEquipped == true)
        {
            anim.SetInteger("equipe", 1);

            if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.W) && isGrounded)
            {
                anim.SetInteger("jumpTrig", 3);
            }
            if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && isGrounded)
            {
                anim.SetInteger("jumpTrig", 3);
            }
            if (Input.GetKey(KeyCode.Space))
            {
                anim.SetInteger("jumpTrig", 3);
            }
            else
            {
                anim.SetInteger("jumpTrig", 0); // changed 4 - 0 **reminder
            }


            //Vertical  movement animation triggers
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetInteger("vertMove", 3);
            }
            else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
            {
                anim.SetInteger("vertMove", 0);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                anim.SetInteger("vertMove", 4);
            }

            //Sprint Animation Triggeres
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && isGrounded)
            {
                anim.SetInteger("runVert", 2);
            }
            else
            {
                anim.SetInteger("runVert", 0);
            }

            //Horizontal  movement animation triggers
            if (Input.GetKey(KeyCode.A))
            {
                anim.SetInteger("horizonMove", 3);
            }
            else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                anim.SetInteger("horizonMove", 0);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                anim.SetInteger("horizonMove", 4);
            }

            

        }
    }

    private void magicStateMoves()
    {
        if (throwObj.magicState == true)
        {
                anim.SetInteger("magicState", 1);

                if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.W) && isGrounded)
                {
                    anim.SetInteger("jumpTrig", 4);
                }
                if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && isGrounded)
                {
                    anim.SetInteger("jumpTrig", 4);
                }
                if (Input.GetKey(KeyCode.Space))
                {
                    anim.SetInteger("jumpTrig", 4);
                }
                else
                {
                    anim.SetInteger("jumpTrig", 0); // changed 4 - 0 **reminder
                }


                //Vertical  movement animation triggers
                if (Input.GetKey(KeyCode.W))
                {
                    anim.SetInteger("vertMove", 5);
                }
                else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
                {
                    anim.SetInteger("vertMove", 0);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    anim.SetInteger("vertMove", 6);
                }

                //Sprint Animation Triggeres
                if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && isGrounded)
                {
                    anim.SetInteger("runVert", 3);
                }
                else
                {
                    anim.SetInteger("runVert", 0);
                }

                //Horizontal  movement animation triggers
                if (Input.GetKey(KeyCode.A))
                {
                    anim.SetInteger("horizonMove", 5);
                }
                else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
                {
                    anim.SetInteger("horizonMove", 0);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    anim.SetInteger("horizonMove", 6);
                }
            //
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("magicJump");
                anim.SetBool("ma", true);
            }
            else 
            {
                anim.SetBool("ma", false);
            }
        }
        else if(throwObj.magicState != true)
        {
            anim.SetInteger("magicState", 0);
        }
    }

    void ComboStarter()
    {
        if (isEquipped)
        {
            if (canClick)
            {
                noOfClicks++;
            }
            if (noOfClicks == 1)
            {
                anim.SetInteger("attack", 1);
            }
        }

    }

    public void ComboCheck()
    {

        canClick = false;

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Sword And Shield Slash") && noOfClicks == 1)
        {//If the first animation is still playing and only 1 click has happened, return to idle
            anim.SetInteger("attack", 0);
            canClick = true;
            noOfClicks = 0;
            combatSFX.clip = atk1;
            combatSFX.Play();
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Sword And Shield Slash") && noOfClicks >= 2)
        {//If the first animation is still playing and at least 2 clicks have happened, continue the combo          
            anim.SetInteger("attack", 2);
            canClick = true;
            combatSFX.clip = atk2;
            combatSFX.Play();
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Sword And Shield Slash 0") && noOfClicks == 2)
        {  //If the second animation is still playing and only 2 clicks have happened, return to idle         
            anim.SetInteger("attack", 0);
            canClick = true;
            noOfClicks = 0;
            combatSFX.clip = atk2;
            combatSFX.Play();
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Sword And Shield Slash 0") && noOfClicks >= 3)
        {  //If the second animation is still playing and at least 3 clicks have happened, continue the combo         
            anim.SetInteger("attack", 3);
            canClick = true;
            combatSFX.clip = atk3;
            combatSFX.Play();
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Sword And Shield Attack"))
        { //Since this is the third and last animation, return to idle          
            anim.SetInteger("attack", 0);
            canClick = true;
            noOfClicks = 0;
        }
    }


}
