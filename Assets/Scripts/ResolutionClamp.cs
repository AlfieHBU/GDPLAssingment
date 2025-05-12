using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionClamp : MonoBehaviour
{
    //Max allowed screen resolution
    public int maxWidth = 1920;
    public int maxHeight = 1080;

    //Ensures that only one ResolutionClamp exists
    private static ResolutionClamp instance;

    void Awake()
    {
        //If a instance does not exist then creates an instance and makes sure that it is not destroyed between scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            //Destroys duplicate instances
            Destroy(gameObject);
            return;
        }
    }
    void Start()
    {
        //Clamps screen resolution to the specified maximum dimensions
        int targetWidth = Mathf.Min(Screen.currentResolution.width, maxWidth);
        int targetHeight = Mathf.Min(Screen.currentResolution.height, maxHeight);
        Screen.SetResolution(targetWidth, targetHeight, false);
    }
}
