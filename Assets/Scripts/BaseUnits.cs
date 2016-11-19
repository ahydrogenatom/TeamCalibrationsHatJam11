using UnityEngine;
using System.Collections;

public class BaseUnits : MonoBehaviour {

    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected Animator anim;
    protected float raycastDistance = 0.1f;
    protected float raycastOffset = 0.4f;
    public Transform greayscalePlane;
    public float speed;


    // Use this for initialization
    void Start ()
    {
    //assign rb variable once it points towards the Riged body owner
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
       anim = GetComponent<Animator>();
    }

    protected void Move(float horizontalInput, float verticalInput)
    {
        //set the velocity to a vertor and then applys the velocity and then apply the actual velocity to the rb
        Vector2 vel = rb.velocity;
        vel.x = horizontalInput * speed;
        vel.y = verticalInput * speed;
        rb.velocity = vel;

        if (horizontalInput < 0)
        {
            sr.flipX = true;
        }
        if (horizontalInput > 0)
        {
            sr.flipX = false;
        }

        //set the animator input of speed to the velocity input of inout
        //animator will handle transition from idel to run
        //send the absolute vvalue of input so when we run left it still plays the animation
        //anim.SetFloat("SpeedHorizontal", Mathf.Abs(horizontalInput));
      //  anim.SetFloat("SpeedVertical", Mathf.Abs(verticalInput));
    }

    protected void GreyScale(float offsetX)
    {
        Vector3 origin = transform.position;
        origin.x += offsetX;
        //create a line that shoots down from the feet of the unit, if there was no colider under our unit return false else return true
        RaycastHit2D hitInfo = Physics2D.Raycast(origin, new Vector2(0, -1), raycastDistance);
        //this will draw a line in our screen simiar to the raycast
        Debug.DrawRay(origin, new Vector2(0, -raycastDistance)); 
        greayscalePlane.GetComponent<Renderer>().material.SetVector("_PlayerPos", hitInfo.point);
    }
}
