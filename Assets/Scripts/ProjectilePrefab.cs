using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProjectilePrefab : MonoBehaviour
{
    public float Yfall = -50f;
    public ProjectileBehaviour projectilebehaviour;
    public GameObject impactEffect;
    public AudioClip impactSound;

    public float timeBeforeDespawn = 10f;
    private float timer = 0f;
    private bool hasBeenFired = false;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null && rb.useGravity) 
        {
            hasBeenFired = true;
        }
    }

    void Update()
    {
        if (transform.position.y < Yfall)
        {
            SelfDestruct();
        }

        if (hasBeenFired) 
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null && rb.useGravity) 
            {
                hasBeenFired = true;
            }
        }

        if (hasBeenFired) 
        {
            timer += Time.deltaTime;
            if (timer >= timeBeforeDespawn) 
            {
                SelfDestruct();
            }
        }
    }

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

    private void OnCollisionEnter(Collision collision) 
    {
        string tag = collision.gameObject.tag;

        if (tag == "Enemy")
        {
            PlayImpactEffects(collision);
            Destroy(collision.gameObject);
            int remaining = GameObject.FindGameObjectsWithTag("Enemy").Length - 1;
            FindObjectOfType<UIUpdate>()?.UpdateTargetCount(remaining);
            FindObjectOfType<GameManager>()?.TargetDestroyed();
            SelfDestruct();
        }
        else if (tag == "Ammo_Pickup") 
        {
            PlayImpactEffects(collision);
            projectilebehaviour?.AddAmmo(3);
            Destroy(collision.gameObject);
            SelfDestruct();
        }
    }

    void SelfDestruct() 
    {
        projectilebehaviour.OnProjectileDestroyed();
        Destroy(gameObject);
    }
}