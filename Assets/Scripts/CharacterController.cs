using UnityEngine;
using System.Collections;

public class CharacterController : MovingCharacter {


    private bool isCaught;
    public int jumpHeight;

	
	// Update is called once per frame
	void Update () {

        //move horizontally
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        Move(horizontalInput);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        
	
	}

    //Jump method
    protected void Jump()
    {
        Vector2 vel = rb.velocity;

        vel.y = jumpHeight;

        //vel.x = (float)(vel.x * 1.2);

        rb.velocity = vel;

       // anim.SetBool("InAir", true);


    }
}
