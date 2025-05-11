using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject loseScreen;
    public int targetCount = 0;
    public static bool playerWon = false;
    private bool hasgameEnded = false;

    void Start()
    {

        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "ResultsScene")
        {
            if (GameManager.playerWon)
            {
                winScreen.SetActive(true);
                loseScreen.SetActive(false);
            }
            else
            {
                winScreen.SetActive(false);
                loseScreen.SetActive(true);
            }

        }
     }

    public void PlayGame() 
    {
        //Loads next active scene in the build manager for the main menu.

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu() 
    {
        //Loads Main Menu scene as the Main Menu is under '0' in build manager.
        SceneManager.LoadScene(0);
    }

    public void WinGame() 
    {
        if (hasgameEnded) return;
        hasgameEnded = true;
        playerWon = true;
        SceneManager.LoadScene("ResultsScene");
    }

    public void LoseGame() 
    {
        if (hasgameEnded) return;
        hasgameEnded = true;
        StartCoroutine(LoseAfterDelay(2f));
    }

    private IEnumerator LoseAfterDelay(float delay) 
    {
        yield return new WaitForSeconds(delay);
        playerWon = false;
        SceneManager.LoadScene("ResultsScene");
    }

    public void QuitGame() 
    {
        //Quits application on button press as well as logs in debug log for clarification in development.
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void TargetDestroyed() 
    {
        targetCount--;

        if (targetCount <= 0) 
        {
            WinGame();
        }
    }
}
