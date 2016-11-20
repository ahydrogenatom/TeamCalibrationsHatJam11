using UnityEngine;
using System.Collections;

public class CharacterController : MovingCharacter {


    private bool isCaught;
    [HideInInspector]
    public static bool lightOn = true;
    public int jumpHeight;

    public float bounceModifier;


    


    // Update is called once per frame
    void Update () {


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
