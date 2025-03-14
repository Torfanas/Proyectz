using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    [Header("Puntos de movimiento")]
    public Transform puntoA;
    public Transform puntoB;
    public float velocidad = 3f;

    private Vector3 destinoActual;
    private Transform jugador;
    private CharacterController jugadorController;
    private Vector3 ultimoPosicionPlataforma;

    void Start()
    {
        destinoActual = puntoB.position;
        ultimoPosicionPlataforma = transform.position;
    }

    void Update()
    {
        // Guarda la posición antes de moverse
        Vector3 posicionAnterior = transform.position;

        // Mueve la plataforma
        transform.position = Vector3.MoveTowards(transform.position, destinoActual, velocidad * Time.deltaTime);

        // Calcula el desplazamiento de la plataforma
        Vector3 desplazamiento = transform.position - posicionAnterior;

        // Si el jugador está sobre la plataforma, moverlo con ella
        if (jugador != null && jugadorController != null)
        {
            jugadorController.Move(desplazamiento);
        }

        // Cambia de dirección cuando llega al destino
        if (Vector3.Distance(transform.position, destinoActual) < 0.1f)
        {
            destinoActual = (destinoActual == puntoB.position) ? puntoA.position : puntoB.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugador = other.transform;
            jugadorController = jugador.GetComponent<CharacterController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugador = null;
            jugadorController = null;
        }
    }
}
