using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class UIUpdate : MonoBehaviour
{
    //UI text elements for stats and notifying the player
    public TextMeshProUGUI targetsLeftText;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI powerText;
    public TextMeshProUGUI yText;
    public TextMeshProUGUI xText;


    void Start()
    {
        //Initialize target count at the start of the game
        int initialTargets = GameObject.FindGameObjectsWithTag("Enemy").Length;
        UpdateTargetCount(initialTargets);
        //Force UI to update values
        Canvas.ForceUpdateCanvases();
    }

    //Display ammo count
    public void UpdateAmmo(int count)
    {
        ammoText.text = "SHOTS LEFT: " + count;
        Canvas.ForceUpdateCanvases();
    }

    //Display remaining enemies
    public void UpdateTargetCount(int count)
    {
        targetsLeftText.text = "TARGETS LEFT: " +  count;
        Canvas.ForceUpdateCanvases();
    }

    //Display power of projectile
    public void UpdatePower(float power) 
    {
        powerText.text = "POWER: " + Mathf.RoundToInt(power / 10f);
        Canvas.ForceUpdateCanvases();
    }

    //Updates UI with current angles for aiming
    public void UpdateAngles(float yaw, float pitch)
    {
        yText.text = "ELEVATION ANGLE: " + Mathf.RoundToInt(pitch) + "°";
        Canvas.ForceUpdateCanvases();
        xText.text = "HORIZONTAL ANGLE: " + Mathf.RoundToInt(yaw) + "°";
        Canvas.ForceUpdateCanvases();
    }
}

 