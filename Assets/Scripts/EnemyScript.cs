using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyScript : MonoBehaviour
{
    //Y axis threshold to determine how far an enemy must fall before being destroyed
    public float Yfall = -10f;
    void Update()
    {
        //Destroy the enemy if it falls below the Y threshold
        if (transform.position.y < Yfall)
            Destroy(gameObject);
    }
     void OnTriggerEnter(Collider other)
    {
        //Destroy enemy if hit by player projectile
        if (other.CompareTag("PlayerProjectile")) 
            Destroy(gameObject);
    }
}
