
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class ProjectileBehaviour : MonoBehaviour
{
    //Allows for keybinds to be changed within unity inspector
    public KeyCode pressUp;
    public KeyCode pressDown;
    public KeyCode pressLeft;
    public KeyCode pressRight;
    public KeyCode Fire;
    public KeyCode PowerUp;
    public KeyCode PowerDown;

    //Rotation speed
    public float rotationSpeed = 100f;
    //Applied Force to fired projectiles
    public float fireForce = 0.5f;
    //Allows for one 
    public int maxAmountOfProjectiles = 10;
    private int currentProjectileCount = 0;
   
    //Clamp Boundries for the X and Y axis
    public float Xmin = 0f;
    public float Xmax = 89f;
    public float Ymin = -89f;
    public float Ymax = 89f;
    public float Zmin = -89f;
    public float Zmax = 89f;

    //Tracks the rotation of the object in the X axis to help out the clamp function 
    private float pitch = 0f;
    private float yaw = 0f;
    private float roll = 0f;

    //References
    public GameObject projectilePrefab;
    public Transform firePoint;
    public LineRenderer lineRenderer;

    //
    public int maxAmmo = 10;
    private int currentAmmo;

    public void OnProjectileDestroyed()
    {
        currentProjectileCount = Mathf.Max(0, currentProjectileCount - 1);
    }

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void DrawTrajectory()
    {
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer is not assigned");
            return;
        }

        int resolution = 30;
        Vector3[] points = new Vector3[resolution];

        Vector3 startPosition = firePoint.position;
        Vector3 startVelocity = firePoint.forward * fireForce;

        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;

        for (int i = 0; i < resolution; i++)
        {
            float time = i * 0.1f;
            Vector3 point = startPosition + startVelocity * time + 0.5f * Physics.gravity * time * time;
            points[i] = point;
        }

        lineRenderer.positionCount = resolution;
        for (int i = 0; i < points.Length; i++) 
        {
            lineRenderer.SetPosition(i, points[i]);
        }

        Debug.Log("FirePoint Position: " + firePoint.position);
    }

    void Update()
    {
        DrawTrajectory();

        float Yrotation = 0f;
        float Xrotation = 0f;
        float Zrotation = 0f;

        //Rotate Left
        if (Input.GetKey(pressLeft))
            Yrotation -= rotationSpeed;

        //Rotate Right
        if (Input.GetKey(pressRight))
            Yrotation += rotationSpeed;

        //Rotate Up
        if (Input.GetKey(pressUp))
            Xrotation -= rotationSpeed;

        //Rotate Down
        if (Input.GetKey(pressDown))
            Xrotation += rotationSpeed;

        //Fire Power Up
        if (Input.GetKey(PowerUp))
            fireForce += 10f * Time.deltaTime;
        //Fire Power Down
        if (Input.GetKey(PowerDown))
            fireForce -= 10f * Time.deltaTime;

        //Pitch, Yaw and Roll update
        pitch += Xrotation * Time.deltaTime;
        yaw += Yrotation * Time.deltaTime;
        roll += Zrotation * Time.deltaTime;

        //Clamp X and Y axis
        pitch = Mathf.Clamp(pitch, Xmin, Xmax);
        yaw = Mathf.Clamp(yaw, Ymin, Ymax);
        roll = Mathf.Clamp(roll, Zmin, Zmax);

        Vector3 targetRotation = new Vector3(pitch, yaw, roll);
        transform.localEulerAngles = targetRotation;

        fireForce = Mathf.Clamp(fireForce, 0f, 100f);

        UIUpdate uiUpdater = FindObjectOfType<UIUpdate>();
        if (uiUpdater != null) 
        {
            uiUpdater.UpdatePower(fireForce);
            uiUpdater.UpdateAngles(yaw, pitch);
        }

        //When fire is pressed (Spacebar = fire as seen within the Unity Inspector)
        if (Input.GetKeyUp(Fire) && currentProjectileCount < 1 && currentAmmo > 0)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
            if (rb != null) 
            {
                rb.velocity = firePoint.forward * fireForce;
            }

            //Projectile increment count
            currentProjectileCount++;
            currentAmmo--;

            //Destroys any Projectile Clone for optimisation

            
            
            
        }
     
        
    }

 



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ammo_Pickup")) 
        {
            currentAmmo++;
            currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmmo);
            Destroy(other.gameObject);
        }
    }


}