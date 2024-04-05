using UnityEngine;
using UnityEngine.SceneManagement;

public class levelComplete : MonoBehaviour
{
    public int sceneBuildIndex;

    private bool player1Entered = false;
    private bool player2Entered = false;

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
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
    }
}

