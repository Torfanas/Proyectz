using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Si el jugador toca el checkpoint
        {
            other.GetComponent<PlayerController>().GuardarCheckpoint(transform.position);
        }
    }
}