using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour {

    private bool winGame;
    public double victoryTimer = 3;
    private double currentTimer;
    private double lastTimer;

    void Update()
    {
        if (winGame == true)
        {
            currentTimer = Time.time - lastTimer;
            if (currentTimer > victoryTimer)
            {
                SceneManager.LoadScene("Credits");
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
