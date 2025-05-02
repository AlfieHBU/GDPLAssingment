using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectilePrefab : MonoBehaviour
{
    public float Yfall = 0f;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
        

        if (collision.gameObject.CompareTag("Ammo_Pickup"))
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);

        {
            if (transform.position.y < Yfall)
                Destroy(gameObject);
        }
    }
}
