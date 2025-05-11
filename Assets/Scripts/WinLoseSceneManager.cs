using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseSceneManager : MonoBehaviour
{
    public GameObject Win_Canvas;
    public GameObject Lose_Canvas;

    void Start()
    {
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

    public void RestartGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame() 
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
