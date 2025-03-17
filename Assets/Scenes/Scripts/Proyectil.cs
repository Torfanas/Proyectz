using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 3f;
    public LayerMask hitLayers;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if ((hitLayers.value & (1 << other.gameObject.layer)) > 0)
        {
            Debug.Log("Impacto en {other.gameObject.name} en la posición {other.transform.position} con el proyectil {gameObject.name}!");

        }

    }
}
