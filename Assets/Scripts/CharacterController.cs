using UnityEngine;
using System.Collections;

public class CharacterController : MovingCharacter {


    private bool isCaught;
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


        //timing code for sanity decay and growth

        //SANITY DECAY CODE
        //decay sanity when light is off
        if(lightOn == false)
        {
            currentDecayTime = Time.time - lastDecayTime;

            //if it is time for sanity decay
            if(currentDecayTime > sanityDecayThreshold)
            {
                if(currentSanity > 0)
                {
                    currentSanity -= sanityDecayAmount;
                }
                
                lastDecayTime = Time.time;
            }
        }

        //SANITY GROWTH CODE
        //increment sanity back up when the light is on
        if(lightOn == true)
        {
            currentGrowthTime = Time.time - lastGrowthTime;

            //if it is time for sanity growth
            if(currentGrowthTime > sanityGrowthThreshold)
            {
                //make sure sanity doesn't go above 100
                if(currentSanity < 100)
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
        
        anim.SetFloat("PlayerSpeed", Mathf.Abs(rb.velocity.x));
		anim.SetFloat ("Vertical speed", rb.velocity.y);


    }

    //Jump method
    protected void Jump()
    {
        Vector2 vel = rb.velocity;

        vel.y = jumpHeight;

        vel.x = (float)(vel.x * 1.2);

        rb.velocity = vel;
	

    }

<<<<<<< HEAD
    //check to see if there is a collision object immediately under the character
    //return true if the character is standing on a collision object
    protected bool CheckIsGrounded(float offsetX)
    {
        Vector3 origin = transform.position;
        origin.x += offsetX;
        //create a downwards-facing raycast at the character's feet
        RaycastHit2D hitInfo = Physics2D.Raycast(origin, new Vector2(0, -1), raycastDistance);

        //draws a line on screen to visually see what the raycast is doing
        Debug.DrawRay(origin, new Vector2(0, -raycastDistance));


        if (hitInfo.collider == null)
        {
            transform.SetParent(null);
            anim.SetBool("isGrounded", false);
            return false;
        }

        //set transform parent to the moving platform if colliding with one
        if ((hitInfo.collider.gameObject.GetComponent<MovingPlatform>() != null))
        {
            //change the parent of this unit to the moving platform it is on
            transform.SetParent(hitInfo.collider.transform);
            anim.SetBool("isGrounded", true);
            return true;
        }

        //make sure player is in a grounded state while on the stairs
        else if((hitInfo.collider.gameObject.GetComponent<Stairs>() != null))
        {
            anim.SetBool("isGrounded", true);
        }
        else
        {
            transform.SetParent(null);
        }

        anim.SetBool("isGrounded", true);
        return true;
    }

=======
>>>>>>> dbfd1aade6f03c0eb1fad03862b387b19c1be947


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


    public void updateSanity()
    {

    }
}
