using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Win/Lose states
    public GameObject winScreen;
    public GameObject loseScreen;
    //Tracks how many targets are remaining
    public int targetCount = 0;
    //Flag that determines if the player has achieved win state or not
    public static bool playerWon = false;
    //Makes sure that the game has ended, only running game-ending logic once
    private bool hasgameEnded = false;

    void Start()
    {
        //Check if the player is in the results scene and show the correct canvas/screen depending on the win/lose state
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

    //Loads the scene under '1' in the Build Settings
    public void PlayGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Restarts scene
    public void RestartGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Loads Main Menu scene as the Main Menu is under '0' in build manager.
    public void MainMenu() 
    {
        SceneManager.LoadScene(0);
    }

    //Handles winning the game and transitioning to the results scene
    public void WinGame() 
    {
        if (hasgameEnded) return;
        hasgameEnded = true;
        playerWon = true;
        SceneManager.LoadScene("ResultsScene");
    }

    //Handles losing the game and transitions to the results scenee after a small delay (2 Seconds)
    public void LoseGame() 
    {
        if (hasgameEnded) return;
        hasgameEnded = true;
        StartCoroutine(LoseAfterDelay(2f));
    }

    //Waits for a delay before triggering lose result
    private IEnumerator LoseAfterDelay(float delay) 
    {
        yield return new WaitForSeconds(delay);
        playerWon = false;
        SceneManager.LoadScene("ResultsScene");
    }

    //Quits application on button press as well as logs in debug log for clarification in development.
    public void QuitGame() 
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    //Checks for a win condition when a target is destoryed
    public void TargetDestroyed() 
    {
        targetCount--;

        if (targetCount <= 0) 
        {
            WinGame();
        }
    }
}
