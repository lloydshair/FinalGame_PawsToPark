using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsScript : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(4);
    }
}
