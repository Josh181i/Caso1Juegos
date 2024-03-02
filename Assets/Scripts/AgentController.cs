using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

// Ataque de Agente vale 25 PUNTOS
public class AgentController : MonoBehaviour
{
    [SerializeField]
    Transform target;

    // Distancia minima

    NavMeshAgent _navAgent;
    private void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        // Si la distancia es menor o igual a la distancia minima entonces ataque
        _navAgent.SetDestination(target.position);
    }
}
