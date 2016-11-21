using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScaryFaceController : MonoBehaviour {

    float zPos;
    Vector3 cameraPosition;
    SpriteRenderer sr;

    bool isEatingScreen;
	// Use this for initialization
	void Start () {

        isEatingScreen = false;
        sr = GetComponent<SpriteRenderer>();
        //set position of face to camera
        cameraPosition = new Vector3 (transform.parent.transform.position.x, transform.parent.transform.position.y);

        Camera camera = GetComponent<Camera>();

        zPos = 10;

        setVisible(false);
	
	}
	
	// Update is called once per frame
	void Update () {

        if(isEatingScreen == true)
        {
            Vector3 newPos = new Vector3(transform.position.x, transform.position.y, zPos);
            transform.position = newPos;

            zPos -= 0.3f;
        }

        if(zPos <= -15)
        {
            SceneManager.LoadScene("Level");
        }

        

    }

    //sets the visibility on screen of the scary face
    public void setVisible(bool isVisible)
    {
        sr.enabled = isVisible;
    }


    public void eatScreen()
    {
        isEatingScreen = true;
        
    }
}
