using UnityEngine;
using System.Collections;

public class CharacterController : MovingCharacter {


    private bool isCaught;
    public int jumpHeight;


    


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


    }

    //Jump method
    protected void Jump()
    {
        Vector2 vel = rb.velocity;

        vel.y = jumpHeight;

        vel.x = (float)(vel.x * 1.2);

        rb.velocity = vel;

        //anim.SetFloat("PlayerSpeed")

    }

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
            return false;
        }

        //set transform parent to the moving platform if colliding with one
        if (hitInfo.collider.gameObject.GetComponent<MovingPlatform>() != null)
        {
            //change the parent of this unit to the moving platform it is on
            transform.SetParent(hitInfo.collider.transform);
        }
        else
        {
            transform.SetParent(null);
        }


        return true;
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
