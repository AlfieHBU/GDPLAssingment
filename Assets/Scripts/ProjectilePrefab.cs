using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProjectilePrefab : MonoBehaviour
{
    public float Yfall = -50f;
    public ProjectileBehaviour projectilebehaviour;
    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Enemy")
        {
            Destroy(collision.gameObject);
            int remaining = GameObject.FindGameObjectsWithTag("Enemy").Length - 1;
            FindObjectOfType<UIUpdate>()?.UpdateTargetCount(remaining);
            SetInactiveAndCleanUp();
        }
        else if (tag == "Ammo_Pickup")
        {
            Destroy(collision.gameObject);
            SetInactiveAndCleanUp();
        }
    }

    private void Update()
    {
        if (transform.position.y < Yfall)
        {
            SetInactiveAndCleanUp();
        }
    }

    private void DeactivateProjectile()
    {
        gameObject.SetActive(false);
        projectilebehaviour?.OnProjectileDestroyed();
        Destroy(gameObject);
    }
    private void SetInactiveAndCleanUp()
    {
        //Sets the projectile as false and destroys as soon as one of the three criteria are met: either colliding with an enemy or ammo pickup or falling below a certain Y value.
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}