using UnityEngine;
using System.Collections;

public class ClownAI : MovingCharacter {

    public static bool greenLight = true;

    public double walkTimer;

    public SoundManager sounds;

    protected float maxRayDistance = 1000;

    private double currentWalkTime;

    private double lastWalkTime;

    public float direction = 1;
    private float idle = 0;

    

    private double movementSwitchtimer;

    private double lightTimer = 5;
    private double currenttimer;
    private double lastTimer;

    public ScaryFaceController scaryFace;
    public FOVConeController visionCone;

	// Update is called once per frame
	void Update () {
        if (greenLight == true)
        {
            anim.SetBool("GreenLight", greenLight);
            Move(idle);
        }
        if (greenLight == false)
        {
            anim.SetBool("GreenLight", greenLight);
            Move(direction);
            currentWalkTime = Time.time - lastWalkTime;
            if (currentWalkTime > walkTimer)
            {
                direction *= -1;
                lastWalkTime = Time.time;
            }
        }

        setLight(lightTimer);

        anim.SetFloat("Movement", Mathf.Abs(rb.velocity.x));
    }

    void setLight(double lightTime)
    {
        currenttimer = Time.time - lastTimer;
        if (currenttimer > lightTime)
        {
            if(greenLight == true)
            {
                greenLight = false;
                walkTimer *= GetRandom();
                
            }
            else
            {
                greenLight = true;
                walkTimer = 10;
            }

            lastTimer = Time.time;
        }
    }

    void OnTriggerEnter2D(Collider2D objectColided)
    {
        if(greenLight == false && objectColided.gameObject.GetComponent<CharacterController>() != null)
        {
            if(CharacterController.lightOn == true && CharacterController.isCaught == false)
            {
                CharacterController.isCaught = true;
                Instantiate(sounds.creepylaughNoise);
                scaryFace.setVisible(true);
                scaryFace.eatScreen();
            }
        }
    }

    void caughtPlayer(float y, float x)
    {
        CharacterController.isCaught = true;
        Vector3 origin = transform.position;
        RaycastHit2D hitInfo = Physics2D.Raycast(origin, new Vector2(x, y), maxRayDistance);
        Debug.DrawRay(origin, new Vector2(x, y));
        
        if(hitInfo.collider.gameObject.GetComponent<CharacterController>() != null)
        {

             
        }
        else
        {
            while (hitInfo.distance > 5)
            {
                hitInfo = Physics2D.Raycast(origin, new Vector2(90, -2), maxRayDistance);
                Move(moveSpeed);
            }
            Move(0);
            //play grab animation
            //   while (AnimationState.time >= AnimationState.length)
            //   {
            //       yield return null;
            //   }

         
        }

    }


}
