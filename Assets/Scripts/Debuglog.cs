using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuglog : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            Debug.Log("W key was pressed.");

        if (Input.GetKeyUp(KeyCode.W))
            Debug.Log("W key was released");

        if (Input.GetKeyDown(KeyCode.A)) 
            Debug.Log("A key was pressed.");

        if (Input.GetKeyUp(KeyCode.A)) 
            Debug.Log("A key was released");

        if (Input.GetKeyDown(KeyCode.S)) 
            Debug.Log("S key was pressed.");

        if (Input.GetKeyUp(KeyCode.S)) 
            Debug.Log("S key was released");

        if (Input.GetKeyDown(KeyCode.D)) 
            Debug.Log("D key was pressed.");

        if (Input.GetKeyUp(KeyCode.D)) 
            Debug.Log("D key was released");

        if (Input.GetKeyDown(KeyCode.Space))
            Debug.Log("Spacebar was pressed.");

        if (Input.GetKeyUp(KeyCode.Space))
            Debug.Log("Spacebar was released");
    }

}
