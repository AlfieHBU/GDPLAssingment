using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class UIUpdate : MonoBehaviour
{
    public TextMeshProUGUI targetsLeftText;
    public TextMeshProUGUI shotsText;
    public TextMeshProUGUI powerText;
    public TextMeshProUGUI yText;
    public TextMeshProUGUI xText;


    void Start()
    {
        int initialTargets = GameObject.FindGameObjectsWithTag("Enemy").Length;
        UpdateTargetCount(initialTargets);
    }

    public void UpdateShotsCount(int count)
    {
        shotsText.text = "SHOTS LEFT: " + count;
    }

    public void UpdateTargetCount(int count)
    {
        targetsLeftText.text = "TARGETS LEFT: " +  count;
    }

    public void UpdatePower(float power) 
    {
        powerText.text = "POWER: " + Mathf.RoundToInt(power / 10f);
    }

    public void UpdateAngles(float yaw, float pitch)
    {
        yText.text = "ELEVATION ANGLE: " + Mathf.RoundToInt(pitch) + "°";
        xText.text = "HORIZONTAL ANGLE: " + Mathf.RoundToInt(yaw) + "°";
    }
}

 