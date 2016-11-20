using UnityEngine;
using System.Collections;

public class FOVConeController : MonoBehaviour {

    SpriteRenderer sr;

    bool isActive;

    Vector3 clownPos;

    float currentDirection;
    float lastDirection;
    // Use this for initialization
    void Start () {

        clownPos = transform.parent.transform.position;
        sr = GetComponent<SpriteRenderer>();
        isActive = !GreenLightManager.getLight();
        
	}
	
	// Update is called once per frame
	void Update () {

        currentDirection = GetComponentInParent<ClownAI>().direction;
        //print(direction);
        if (currentDirection != lastDirection)
        {
            transform.Rotate(new Vector3(0, 0, 180));
            //sr.flipX = false;
        }
        


        if (GreenLightManager.getLight() == true)
        {
            setVisible(false);
        }

        if(GreenLightManager.getLight() == false)
        {
            setVisible(true);
        }

        lastDirection = currentDirection;
	}


    //sets the visibility on screen of the scary face
    public void setVisible(bool isVisible)
    {
        sr.enabled = isVisible;
    }
}
