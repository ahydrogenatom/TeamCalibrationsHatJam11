using UnityEngine;
using System.Collections;

public class ClownAI : MovingCharacter {

    private bool greenLight = true;

    public double walkTimer;



    private double currentWalkTime;

    private double lastWalkTime;

    private float direction = 1;
    private float idle = 0;

    private double movementSwitchtimer;

    private double lightTimer = 5;
    private double currenttimer;
    private double lastTimer;

	// Update is called once per frame
	void Update () {

        if (greenLight == true)
        {
            anim.SetBool("GreenLight", true);
            Move(idle);
        }
        if (greenLight == false)
        {
            anim.SetBool("GreenLight", false);
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
                lightTimer *= GetRandom();
            }
            else
            {
                greenLight = true;
                lightTimer = 10;
            }

            lastTimer = Time.time;
        }
    }
}
