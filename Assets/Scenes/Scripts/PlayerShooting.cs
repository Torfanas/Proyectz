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
                //animacion de apuntado de ser necesario
        }
        else 
        {
            isAiming = false;
        }
    }

    void Shoot()
    {
        GameObject newProjectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(playerCamera.transform.forward * shootForce);
        }
    }
}
