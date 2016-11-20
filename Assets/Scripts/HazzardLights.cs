using UnityEngine;
using System.Collections;

public class HazzardLights : BaseLight {

    public double fireLaunchThreshold;

    private double currentLaunchTime;

    private double lastLaunchTime; 

    private float minLight = 0.65f;
    private float maxLight =20f;
    public float fadeTime = 1;
    private float tParam = 0;




    // Update is called once per frame
    void Update()
    {

        currentLaunchTime = Time.time - lastLaunchTime;
     if(currentLaunchTime > fireLaunchThreshold)
        {
            var temp = minLight;
            minLight = maxLight;
            maxLight = temp;

            lastLaunchTime = Time.time;
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

