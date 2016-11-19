using UnityEngine;
using System.Collections;

public class BaseLight : MonoBehaviour {

    public float rayDistance = 50f;
    public float rayOffset = 0f;
    public Transform greayscalePlane;
    public string lightSource;


  void update()
    {
        GreyScale(rayOffset);
    }

    public void GreyScale(float offsetX)
    {
        Vector3 origin = transform.position;
        origin.x += offsetX;
        //create a line that shoots down from the feet of the unit, if there was no colider under our unit return false else return true
        RaycastHit2D hitInfo = Physics2D.Raycast(origin, new Vector2(0, -1), rayDistance);
        //this will draw a line in our screen simiar to the raycast
        Debug.DrawRay(origin, new Vector2(0, -rayDistance)); 
        greayscalePlane.GetComponent<Renderer>().material.SetVector(lightSource, hitInfo.point);
    }
}
