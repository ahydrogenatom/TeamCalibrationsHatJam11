using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScaryFaceController : MonoBehaviour {

    float zPos;
    Vector3 cameraPosition;
    SpriteRenderer sr;
	// Use this for initialization
	void Start () {

        sr = GetComponent<SpriteRenderer>();
        //set position of face to camera
        cameraPosition = new Vector3 (transform.parent.transform.position.x, transform.parent.transform.position.y);

        Camera camera = GetComponent<Camera>();

        zPos = 10;

        setVisible(false);
	
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, zPos);
        transform.position = newPos;

        zPos -= 0.1f;

    }

    //sets the visibility on screen of the scary face
    public void setVisible(bool isVisible)
    {
        sr.enabled = isVisible;
    }


    public void eatScreen()
    {
        while(zPos > 0)
        {
            Vector3 newPos = new Vector3(transform.position.x, transform.position.y, zPos);
            transform.position = newPos;

            zPos -= 0.1f;
        }

        SceneManager.LoadScene("Level");
    }
}
