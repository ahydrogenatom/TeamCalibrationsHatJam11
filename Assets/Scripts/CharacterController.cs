using UnityEngine;
using System.Collections;

public class CharacterController : MovingCharacter {


    public ScaryFaceController scaryFace;

    public static bool isCaught;
    [HideInInspector]
    public static bool lightOn = true;
    public int jumpHeight;

    public float bounceModifier;

    //SANITY VARS
    //holds the current sanity of the player
    public static int currentSanity = 100;

    //holds the maximim possible sanity value
    public int maxSanity;

    //holds how much sanity is subtracted on every sanity update interval
    public int sanityDecayAmount;

    //holds sanity growth amount
    public int sanityGrowthAmount;


    //TIMER VARS

        //DECAY VARS
    //holds how long it takes to decrement sanity
    public double sanityDecayThreshold;

    //holds the current time between decay cycles
    private double currentDecayTime;

    //holds the time of the last decay tick
    private double lastDecayTime;


    //GROWTH VARS
    //holds how long it takes to increment sanity
    public double sanityGrowthThreshold;

    //holds the current time between growth cycles
    private double currentGrowthTime;

    //holds the time of the last growth tick
    private double lastGrowthTime;



    //returns the current sanity amount
    public static int getSanity()
    {
        return currentSanity;
    }

    //set the current sanity
    public static void setSanity(int newSanity)
    {
        newSanity = currentSanity;
    }

    // Update is called once per frame
    void Update () {


        //check if player is caught by the clowns
        if (isCaught == false)
        {

            //timing code for sanity decay and growth

            //SANITY DECAY CODE
            //decay sanity when light is off
            if (lightOn == false)
            {
                currentDecayTime = Time.time - lastDecayTime;

                //if it is time for sanity decay
                if (currentDecayTime > sanityDecayThreshold)
                {
                    if (currentSanity > 0)
                    {
                        currentSanity -= sanityDecayAmount;
                    }

                    lastDecayTime = Time.time;
                }
            }

            //SANITY GROWTH CODE
            //increment sanity back up when the light is on
            if (lightOn == true)
            {
                currentGrowthTime = Time.time - lastGrowthTime;

                //if it is time for sanity growth
                if (currentGrowthTime > sanityGrowthThreshold)
                {
                    //make sure sanity doesn't go above 100
                    if (currentSanity < 100)
                    {
                        currentSanity += sanityGrowthAmount;
                    }
                    lastGrowthTime = Time.time;
                }
            }

            //print(currentSanity);

            //check to see if character is grounded
            //able to jump if one of the raycasts on either side of character is on the ground
            if (CheckIsGrounded(raycastOffset) || CheckIsGrounded(-raycastOffset))
            {
                isGrounded = true;
            }
            else isGrounded = false;

            //move horizontally
            float horizontalInput = Input.GetAxisRaw("Horizontal");


            //on the ground
            if (isGrounded == true)
            {
                Move(horizontalInput);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                }
            }
            //in the air
            if (isGrounded == false)
            {

                float airMovement = (float)(horizontalInput * 0.8);
                Move(airMovement);

            }

        }


        //set caught behaviour
        if(isCaught == true)
        {
            //play cry animation

        //play loss screen
        }
        anim.SetFloat("PlayerSpeed", Mathf.Abs(rb.velocity.x));
		anim.SetFloat ("Vertical speed", rb.velocity.y);


        if(currentSanity <= 1)
        {
            isCaught = true;
            scaryFace.setVisible(true);
            scaryFace.eatScreen();
        }
        
    }


    

    //Jump method
    protected void Jump()
    {
        Vector2 vel = rb.velocity;

        vel.y = jumpHeight;

        vel.x = (float)(vel.x * 1.2);

        rb.velocity = vel;
	

    }
    
   
    


    void OnTriggerEnter2D(Collider2D myCollider)
    {
        //when hits spikes
        if (myCollider.gameObject.GetComponent<BouncingObjectController>() != null)
        {

            Vector2 bounceJumpBack = rb.velocity;
            
            bounceJumpBack.y = (float)(-bounceJumpBack.y * 1.5);

            rb.velocity = bounceJumpBack;
        }
    }



}
