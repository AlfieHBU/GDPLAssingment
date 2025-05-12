using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseSceneManager : MonoBehaviour
{
    //References to the win and lose canvases that the player will see depending on if they win or lose
    public GameObject Win_Canvas;
    public GameObject Lose_Canvas;

    void Start()
    {
        //Sets active the relevant canvas depending on the final state of the game (winning or losing)
        if (GameManager.playerWon)
        {
            Win_Canvas.SetActive(true);
            Lose_Canvas.SetActive(false);
        }
        else 
        {
            Win_Canvas.SetActive(false);
            Lose_Canvas.SetActive(true);
        }
    }

    //Reloads prior scene to 'restart' the game
    public void RestartGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    //Loads the main menu due to the menu scene being set as '0' within the build settings
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    //Quits the game
    public void QuitGame() 
    {
        Debug.Log("QUIT"); 
        Application.Quit();
    }
}
