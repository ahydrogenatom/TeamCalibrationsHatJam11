using UnityEngine;
using System.Collections;

public class FireManager : MonoBehaviour {


    private double currenttimer;

    private double lastTimer;

    public double lightTime;

    private bool isOn;

    SpriteRenderer sr;
    // Use this for initialization
    void Start () {

        isOn = true;
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        currenttimer = Time.time - lastTimer;
        if (currenttimer > lightTime)
        {
            if (isOn == true)
            {
                setVisible(false);
                isOn = false;
            }
            else
            {
                setVisible(true);
                isOn = true;
            }

            lastTimer = Time.time;
        }
    }


    //sets the visibility on screen of the scary face
    public void setVisible(bool isVisible)
    {
        sr.enabled = isVisible;
    }
}
