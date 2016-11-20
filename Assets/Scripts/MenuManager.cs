using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level");
        
    }

    public void PlayCredits()
    {
        //load credits scene here
        SceneManager.LoadScene("");
    }

    //function to quit the game                                                                                                                                          
    public void QuitGame()
    {
        Application.Quit();
    }
}
