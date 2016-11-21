using UnityEngine;
using System.Collections;

public class Victory : MonoBehaviour {

    private bool winGame;
    public double victoryTimer = 5;
    private double currentTimer;
    private double lastTimer;

    void Update()
    {
        if (winGame == true)
        {
            currentTimer = Time.time - lastTimer;
            if (currentTimer > victoryTimer)
            {
                //Change game sceen
                lastTimer = Time.time;
            }
        }
    }

	void OnTriggerEnter2D(Collider2D otherCollied)
    {
       if(otherCollied.gameObject.GetComponent<CharacterController>() != null)
        {
            var player = otherCollied.gameObject.GetComponent<CharacterController>();
            CharacterController.isCaught = true;
            player.winGame();
            winGame = true;
        }

    }


}
