using UnityEngine;
using System.Collections;

public class PlataformaDesaparece : MonoBehaviour
{
    public float tiempoAntesDeDesaparecer = 2f;  // Tiempo antes de desaparecer
    public float tiempoAntesDeReaparecer = 3f;   // Tiempo antes de reaparecer

    private Renderer renderizador;
    private Collider colisionador;

    void Start()
    {
        renderizador = GetComponent<Renderer>();
        colisionador = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player")) // Verifica si es el jugador
        {
            StartCoroutine(Desaparecer());
        }
    }

    IEnumerator Desaparecer()
    {
        yield return new WaitForSeconds(tiempoAntesDeDesaparecer);
        renderizador.enabled = false;  // Oculta la plataforma
        colisionador.enabled = false;  // Desactiva la colisión

        yield return new WaitForSeconds(tiempoAntesDeReaparecer);
        renderizador.enabled = true;   // Reactiva la plataforma
        colisionador.enabled = true;
    }
}
