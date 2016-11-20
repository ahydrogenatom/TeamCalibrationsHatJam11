using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SanityBarController : MonoBehaviour {

    public Slider sanityBar;

    


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        double sanityBarValue = CharacterController.getSanity();
        sanityBarValue = sanityBarValue / 100;
        sanityBar.value = (float) sanityBarValue;
        print(sanityBarValue);
	}
}
