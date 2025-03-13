using UnityEngine;

public class PickupItem : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Verifica si el jugador entra en contacto con el objeto
        if (other.CompareTag("Player"))
        {
            // añadir al inventario, sumar puntos ...
            Debug.Log("Objeto recogido: " + gameObject.name);

            // Destruir el objeto al recogerlo
            Destroy(gameObject);
        }
    }
}