using UnityEngine;
using System.Collections;

public class BaseLight : MonoBehaviour {

    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected Animator anim;
    protected float raycastDistance = 0.1f;
    protected float raycastOffset = 0.4f;
    public Transform greayscalePlane;


    // Use this for initialization
    void Start ()
    {
    //assign rb variable once it points towards the Riged body owner
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
       anim = GetComponent<Animator>();
    }

    void update()
    {
        GreyScale(raycastOffset);
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
