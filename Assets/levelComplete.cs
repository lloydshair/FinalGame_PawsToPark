using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class levelComplete : MonoBehaviour
{
    public int sceneBuildIndex;

    private bool player1Entered = false;
    private bool player2Entered = false;

    public Animator transition;
    public float transitionTime = 1f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Trigger Entered");

        if (other.CompareTag("Player_01"))
        {
            player1Entered = true;
        }

        if (other.CompareTag("Player_02"))
        {
            player2Entered = true;
        }

        if (player1Entered && player2Entered)
        {
            print("Switching Scene to " + sceneBuildIndex);
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}

