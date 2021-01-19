using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//Adapted from: https://www.youtube.com/watch?v=gXpi1czz5NA&t=1001s

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform  player;
    private Animator anim;

    public Slider healthBar;
    public static bool isHit;
    public bool isAlive;

    public ParticleSystem hitEffect; //particle system

    public AudioSource hitSFX;
    public AudioClip hit;
    public AudioClip score;

    //Animator enemyAnims;

    void Start()
    {
        anim = GetComponent<Animator>();
        isAlive = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(healthBar.value <= 0)
        {
            
            return;
        }

        Vector3 direction = player.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.position);

        if (Vector3.Distance( player.position, this.transform.position)< 20)//DETECTION RANGE
        {
           
            direction.y = 0;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                       Quaternion.LookRotation(direction), 0.08f); // ROTATION / LOOK TOWARDS PLAYER SPEED   

            anim.SetBool("idle", false);

            if(direction.magnitude > 6) // When distance from player is greater than 6 AI runs
            {
                this.transform.Translate(0, 0, 0.07f);
                anim.SetBool("run", true);
                anim.SetBool("attack", false);
                anim.SetBool("walk", false);
                anim.SetBool("hit", false);
            }
            else if (direction.magnitude >= 4)
            {
                this.transform.Translate(0, 0, 0.03f);// When distance from player is greater than 6 AI Walks
                anim.SetBool("run", false);
                anim.SetBool("attack", false);
                anim.SetBool("walk", true);
                anim.SetBool("hit", false);
            }
            else if(direction.magnitude >=2)
            {
                this.transform.Translate(0, 0, 0.02f);// When distance from player is greater than 6 AI Attacks
                anim.SetBool("attack", true);
                anim.SetBool("walk", false);
                anim.SetBool("run", false);
                anim.SetBool("hit", false);
            }
            else if( isHit == true)
            {
                
                anim.SetBool("hit", true);
                anim.SetBool("attack", false);
                anim.SetBool("walk", false);
                anim.SetBool("run", false);
                hitEffect.Play();
                hitSFX.clip = hit;
                hitSFX.Play();
            }

        }
        else
        {
            anim.SetBool("idle", true);
            anim.SetBool("walk", false);
            anim.SetBool("attack", false);
            anim.SetBool("run", false);
            anim.SetBool("hit", false);
        }

        
    }

    private void OnTriggerEnter(Collider enemy)
    {
        if (isAlive == true)
        {

            if (enemy.gameObject.tag == "swords")//Take Damage when hit by GameObjects tagged "swords" <---Weapons
            {
                Debug.Log("HITTING");
                isHit = true; //Triggers the getting hit animation on hit
                healthBar.value -= 25;
               

            }
            else if(enemy.gameObject.tag == "magicBalls")
            {
                Debug.Log("MAGICCCCCC");
                isHit = true;
                healthBar.value -= 75;
                
            }
            else
            {
                isHit = false;
            }

            if (healthBar.value <= 0)//When health is <= 0 triggers death animation and sets alive state to false
            {
                anim.SetBool("isDead", true);
                isAlive = false;
                hitSFX.clip = score;
                hitSFX.Play();
                

            }
        }
        //try is dead is true as well*********
        else if (!isAlive) //When alive state is false i.e. the bot dies call in the "Score" script and add 100 to the default score value <--- Score system
        {
            Score.defaultScore += 100; //Importing the Score Script so that it gets activated everytime an AI bot Dies
        }


    }
}
