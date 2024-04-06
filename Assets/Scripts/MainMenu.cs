using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
   
    public void PlayGame()
    {

        Debug.Log("button is clicked");
        SceneManager.LoadSceneAsync(6);

    }

    public void HowToPlay()
    {

        SceneManager.LoadSceneAsync(2);
    }

    public void Options() {

        SceneManager.LoadSceneAsync(1);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
