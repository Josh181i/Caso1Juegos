using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Implementar sistema de salud al jugador 30 PUNTOS
// * Agentes dañan 5
// * Los objetos que obsilantes dañan 15
// * Pierden la vida reinician la escena 20 PUNTOS
public class HealthController : MonoBehaviour
{
    [SerializeField]
    float maxHealth = 100.00F;

    float _currentHealth;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= Mathf.Abs(damage);
        if (_currentHealth <= 0.0F ) 
        {
            Destroy(gameObject);
        }
    }

    public void Heal(float repair)
    {
        _currentHealth += Mathf.Abs(repair);
    }
}
