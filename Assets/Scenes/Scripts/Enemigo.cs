using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 30;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemigo recibió daño: " + damage);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemigo derrotado");
        Destroy(gameObject);
    }
}