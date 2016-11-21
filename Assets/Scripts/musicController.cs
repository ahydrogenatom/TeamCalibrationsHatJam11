using UnityEngine;
using System.Collections;

public class musicController : MonoBehaviour {

    public AudioSource lightMusic;
    public AudioSource darkMusic;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

        if(GreenLightManager.getLight() == true)
        {
            lightMusic.mute = false;
            darkMusic.mute = true;
        }
        else
        {
            lightMusic.mute = true;
            darkMusic.mute = false;
        }

	}
}
