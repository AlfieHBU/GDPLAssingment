using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUpScript : MonoBehaviour
{
    public float Yfall = -10f;
    void Update()
    {
        if (transform.position.y < Yfall)
            Destroy(gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerProjectile"))
            Destroy(gameObject);
    }
}
