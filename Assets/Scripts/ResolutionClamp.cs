using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionClamp : MonoBehaviour
{
    public int maxWidth = 1920;
    public int maxHeight = 1080;

    private static ResolutionClamp instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
            return;
        }
    }
    void Start()
    {
        int targetWidth = Mathf.Min(Screen.currentResolution.width, maxWidth);
        int targetHeight = Mathf.Min(Screen.currentResolution.height, maxHeight);
        Screen.SetResolution(targetWidth, targetHeight, false);
    }
}
