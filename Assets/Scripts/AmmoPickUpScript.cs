using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUpScript : MonoBehaviour
{
    //Sets the Y threshold to destroy a Ammo Skeleton enemy in the circumstance that the prefab falls below a certain Y amount
    public float Yfall = -10f;
    void Update()
    {
        //Destroys the Ammo Skeleton in the circumstance of it falling below a specific Y position set by 'Yfall'
        if (transform.position.y < Yfall)
            Destroy(gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        //Destroys the Ammo Skeletons if it hit by a player projectile
        if (other.CompareTag("PlayerProjectile"))
            Destroy(gameObject);
    }
}
