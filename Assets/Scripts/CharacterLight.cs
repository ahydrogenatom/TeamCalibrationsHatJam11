using UnityEngine;
using System.Collections;

public class CharacterLight : BaseLight {
        public float smoothTime = 0f;
    // Use this for initialization

    private Vector2 velocity = Vector2.zero;
    private float minLight = 1;
    private float maxLight = 16.4f;
    public float fadeTime;
    private float tParam = 0;




    // Update is called once per frame
    void Update () {
        Vector2 goalPos = target.position;
        transform.position = Vector2.SmoothDamp(transform.position, goalPos, ref velocity, smoothTime);
        GreyScale(offsetY);

        if (Input.GetKeyDown(KeyCode.F))
        {       var temp = minLight;
                minLight = maxLight;
                maxLight = temp;
            if (CharacterController.lightOn == true)
            {
                CharacterController.lightOn = false;
                    
            }
            else
            {
                CharacterController.lightOn = true;
            }
            
            while (tParam < 1)
            {
                tParam += Time.deltaTime * fadeTime;
                var lerpedValue = Mathf.Lerp(maxLight, minLight, tParam);
                darknessplane.GetComponent<Renderer>().material.SetFloat("_ColourMaxRadius", lerpedValue);
            }
            tParam = 0;
        }
        
    }
}
