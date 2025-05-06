using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class UIUpdate : MonoBehaviour
{
    public TextMeshProUGUI targetsLeftText;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI powerText;
    public TextMeshProUGUI yText;
    public TextMeshProUGUI xText;


    void Start()
    {
        int initialTargets = GameObject.FindGameObjectsWithTag("Enemy").Length;
        UpdateTargetCount(initialTargets);
        Canvas.ForceUpdateCanvases();
    }

    public void UpdateAmmo(int count)
    {
        ammoText.text = "SHOTS LEFT: " + count;
        Canvas.ForceUpdateCanvases();
    }

    public void UpdateTargetCount(int count)
    {
        targetsLeftText.text = "TARGETS LEFT: " +  count;
        Canvas.ForceUpdateCanvases();
    }

    public void UpdatePower(float power) 
    {
        powerText.text = "POWER: " + Mathf.RoundToInt(power / 10f);
        Canvas.ForceUpdateCanvases();
    }

    public void UpdateAngles(float yaw, float pitch)
    {
        yText.text = "ELEVATION ANGLE: " + Mathf.RoundToInt(pitch) + "°";
        Canvas.ForceUpdateCanvases();
        xText.text = "HORIZONTAL ANGLE: " + Mathf.RoundToInt(yaw) + "°";
        Canvas.ForceUpdateCanvases();
    }
}

 