using UnityEngine;
using System.Collections;

public class Victory : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D otherCollied)
    {
       if(otherCollied.gameObject.GetComponent<CharacterController>() != null)
        {
            var player = otherCollied.gameObject.GetComponent<CharacterController>();
            
        }

    }


}
