using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Configuración del Disparo")]
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public Camera playerCamera;
    public LayerMask hitLayers;
    public float shootForce = 500f;
    
    private bool isAiming = false;

    void Update()
    {
        Aiming();
        if (isAiming)
        {
            RotateShootPoint();
        }

        if (isAiming && Input.GetMouseButtonDown(0)) 
        {
            Shoot();
        }
    }

    void Aiming()
    {
        if (Input.GetMouseButton(1)) 
        {
            isAiming = true;
        }
        else 
        {
            isAiming = false;
        }
    }

    void RotateShootPoint()
    {
        if (playerCamera != null && shootPoint != null)
        {
            shootPoint.rotation = Quaternion.LookRotation(playerCamera.transform.forward);
        }
    }

    void Shoot()
    {
        GameObject newProjectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(shootPoint.forward * shootForce);
        }
    }
}

