using UnityEngine;
using System.Collections;

public class GreenLightManager : MonoBehaviour {
    private static bool greenLight = true;
    private double currentTimer;
    private double lightTime = 5;
    private double lastLightTimer;

    void start()
    {
        greenLight = true;
        lightTime = 5;
        lastLightTimer = 0;
    }

    public static bool getLight()
    {
        return greenLight;
    }

    void Update()
    {
        currentTimer = Time.time - lastLightTimer;
        if (currentTimer > lightTime)
        {
            if (greenLight == true)
            {
                greenLight = false;
            }
            else
            {
                greenLight = true;
            }
            lastLightTimer = Time.time;
        }
    }
}
