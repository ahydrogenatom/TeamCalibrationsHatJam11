using UnityEngine;
using System.Collections;

public class MovingCharacter : MonoBehaviour {

    public int moveSpeed;
    public int lightRange;

    public float randMin;
    public float randMax;


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

        else if ((hitInfo.collider.gameObject.GetComponent<Stairs>() != null))
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

    protected float GetRandom()
   {
        return Random.Range(randMin, randMax);
   }
}
