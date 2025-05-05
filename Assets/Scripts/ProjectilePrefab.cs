using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePrefab : MonoBehaviour
{
    public float Yfall = 0f;
    public ProjectileBehaviour projectilebehaviour;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);

            int remaining = GameObject.FindGameObjectsWithTag("Enemy").Length - 1;

            FindObjectOfType<UIUpdate>().UpdateTargetCount(remaining);
        }


        if (collision.gameObject.CompareTag("Ammo_Pickup"))
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
 
    private void Update()
    {
        if (transform.position.y < Yfall)
        {
            Destroy(gameObject);
        }
    }

}
