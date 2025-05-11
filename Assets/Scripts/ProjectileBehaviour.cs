using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    //
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
    private GameManager gameManager;
  
   
    public void OnProjectileDestroyed()
    {
        currentProjectileCount = Mathf.Max(0, currentProjectileCount - 1);

        if (currentAmmo <= 0 &&
            currentProjectileCount <= 0 &&
            GameObject.FindGameObjectsWithTag("Enemy").Length > 0) 
        {
            gameManager?.LoseGame();
        }
    }

    public void AddAmmo(int ammo) 
    {
        currentAmmo += ammo;
        FindObjectOfType<UIUpdate>()?.UpdateAmmo(currentAmmo);
    }

    void Start()
    {
        currentAmmo = maxAmmo;
        gameManager = FindObjectOfType<GameManager>();
        UIUpdate uiUpdater = FindObjectOfType<UIUpdate>();
        if (uiUpdater != null)
        {
            uiUpdater.UpdateAmmo(currentAmmo);
        }
    }

    void DrawTrajectory()
    {
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer is not assigned");
            return;
        }

        //How many points are being drawn along the line, resolution is set to not have the trajectory line go the entire way, making the game more challenging.
        int resolution = 10;
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
        if (Input.GetKeyDown(PowerUp))
            fireForce += 10f;
        //Fire Power Down
        if (Input.GetKeyDown(PowerDown))
            fireForce -= 10f;

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
        if (Input.GetKeyUp(Fire) && currentProjectileCount < maxAmountOfProjectiles && currentAmmo > 0)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
            if (rb != null ) 
            {
                rb.isKinematic = true;
            }

            ProjectilePrefab projectileprefabSript = newProjectile.GetComponent<ProjectilePrefab>();
            if (projectileprefabSript != null) 
            {
                projectileprefabSript.projectilebehaviour = this;
            }

            //Projectile increment count
            currentProjectileCount++;
            currentAmmo--;
            
            if (uiUpdater != null) 
            {
                uiUpdater.UpdateAmmo(currentAmmo);
            }

            Debug.Log("Ammo after firing: " + currentAmmo);

            StartCoroutine(FireProjectileAfterDelay(newProjectile, rb));
        }
    }

    private IEnumerator FireProjectileAfterDelay(GameObject newProjectile, Rigidbody rb) 
    {
        yield return null;

        if (rb != null) 
        {
            rb.isKinematic = false;
            rb.AddForce(firePoint.forward * fireForce, ForceMode.Impulse);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ammo_Pickup"))
        {
            currentAmmo++;
            UIUpdate uiUpdater = FindObjectOfType<UIUpdate>();
            uiUpdater?.UpdateAmmo(currentAmmo);
            Destroy(other.gameObject);
            
        }
    }
}