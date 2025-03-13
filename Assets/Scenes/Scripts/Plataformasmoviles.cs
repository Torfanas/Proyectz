using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    [Header("Puntos de Movimiento")]
    public Transform puntoA;
    public Transform puntoB;
    public float velocidad = 2f;

    private Vector3 destino;

    void Start()
    {
        destino = puntoB.position; // Inicia yendo hacia el punto B
    }

    void Update()
    {
        // Mueve la plataforma hacia el destino
        transform.position = Vector3.MoveTowards(transform.position, destino, velocidad * Time.deltaTime);

        // Si llega al destino, cambia de dirección
        if (Vector3.Distance(transform.position, destino) < 0.1f)
        {
            destino = (destino == puntoA.position) ? puntoB.position : puntoA.position;
        }
    }
}