using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryScript : MonoBehaviour
{
    public void Next()
    {
        SceneManager.LoadSceneAsync(3);
    }
}
