using UnityEngine;
using System.Collections;

public class CharacterLight : BaseLight {
        public float smoothTime = 0f;
    // Use this for initialization

    private Vector2 velocity = Vector2.zero;
  
	
	// Update is called once per frame
	void Update () {
        Vector2 goalPos = target.position;
        transform.position = Vector2.SmoothDamp(transform.position, goalPos, ref velocity, smoothTime);
        GreyScale(offsetY);
    }
}
