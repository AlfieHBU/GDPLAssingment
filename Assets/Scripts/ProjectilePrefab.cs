using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProjectilePrefab : MonoBehaviour
{
    //Y position beelow which the projectile is destroyed past the Y value
    public float Yfall = -50f;
    //Reference to the 'Projectile Behaviour' script to refer back to
    public ProjectileBehaviour projectilebehaviour;
    //Sounds and effects that happen on impact
    public GameObject impactEffect;
    public AudioClip impactSound;

    //Time in seconds before projectile despawns if it already has not
    public float timeBeforeDespawn = 10f;
    private float timer = 0f;
    //Tracks if projectile has already fired
    private bool hasBeenFired = false;

    void Start()
    {
        //Detects if projectile has fired via gravity on Rigidbody
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null && rb.useGravity) 
        {
            hasBeenFired = true;
        }
    }

    void Update()
    {
        //If the projectile falls below a determined Y value then the projectile is destroyed
        if (transform.position.y < Yfall)
        {
            SelfDestruct();
        }

        //Firing state check
        if (hasBeenFired) 
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null && rb.useGravity) 
            {
                hasBeenFired = true;
            }
        }

        //Despawns projectile after a time delay if fired
        if (hasBeenFired) 
        {
            timer += Time.deltaTime;
            if (timer >= timeBeforeDespawn) 
            {
                SelfDestruct();
            }
        }
    }

    //On impact plays visual and audio effects
    void PlayImpactEffects(Collision collision) 
    {
        if (impactSound != null)
        {
            AudioSource.PlayClipAtPoint(impactSound, transform.position);
        }

        if (impactEffect != null) 
        {
            ContactPoint contact = collision.contacts[0];
            Instantiate(impactEffect, contact.point, Quaternion.LookRotation(contact.normal));
        }
    }

    //Handles projectile collision logic
    private void OnCollisionEnter(Collision collision) 
    {
        string tag = collision.gameObject.tag;

        if (tag == "Enemy")
        {
            PlayImpactEffects(collision);
            Destroy(collision.gameObject);
            //Updates UI and GameManager regarding remaining enemies
            int remaining = GameObject.FindGameObjectsWithTag("Enemy").Length - 1;
            FindObjectOfType<UIUpdate>()?.UpdateTargetCount(remaining);
            FindObjectOfType<GameManager>()?.TargetDestroyed();
            SelfDestruct();
        }
        else if (tag == "Ammo_Pickup") 
        {
            PlayImpactEffects(collision);
            //Refills ammo when hitting pickup by determined amount (3)
            projectilebehaviour?.AddAmmo(3);
            Destroy(collision.gameObject);
            SelfDestruct();
        }
    }

    //Destroys the projectile
    void SelfDestruct() 
    {
        projectilebehaviour.OnProjectileDestroyed();
        Destroy(gameObject);
    }
}