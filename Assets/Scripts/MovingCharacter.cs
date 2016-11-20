using UnityEngine;
using System.Collections;

public class MovingCharacter : MonoBehaviour {

    public int moveSpeed;
    public int lightRange;
    
    

    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected Animator anim;
    protected BoxCollider2D bc;




    protected float raycastDistance = 0.1f;
    protected float raycastOffset = 0.4f;


    protected bool isGrounded;

    // Use this for initialization
    void Start()
    {

        //assign the private RigidBody component
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
    }

    protected void Move(float input)
    {
        //create a temporary variable to alter velocity
        Vector2 vel = rb.velocity;

        vel.x = input * moveSpeed;

        rb.velocity = vel;


        //face left if horiz. movement is negative
        if (input < 0)
        {
            sr.flipX = true;
        }
        else if (input > 0)
        {
            sr.flipX = false;
        }

        //send the animator the absolute value of current character speed
        //anim.SetFloat("Speed", Mathf.Abs(input));
    }
}
