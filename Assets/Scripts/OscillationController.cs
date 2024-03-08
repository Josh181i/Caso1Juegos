using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillationController : MonoBehaviour
{
    [SerializeField] bool backAndForth;
    [SerializeField] bool reverse;
    [SerializeField] bool moveInX;
    [SerializeField] bool moveInY;
    [SerializeField] bool moveInZ;
    [SerializeField] float travelDistance;
    [SerializeField] float speed;

    [SerializeField] float timeBetweenAttacks = 1f;
    bool alreadyAttacked;

    [SerializeField] float damagePerAttack = 15f;

    private Vector3 _startPosition;

    [SerializeField] Vector3 gizmoPosition;

    [SerializeField] Vector3 attackPositionOffset;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        if (!backAndForth)
        {
            return;
        }

        Vector3 position = transform.position;

        if (moveInX)
        {
            position.x = _startPosition.x + Mathf.PingPong(Time.time * speed, travelDistance) * (reverse ? -1 : 1);
        }

        if (moveInY)
        {
            position.y = _startPosition.y + Mathf.PingPong(Time.time * speed, travelDistance) * (reverse ? -1 : 1);
        }

        if (moveInZ)
        {
            position.z = _startPosition.z + Mathf.PingPong(Time.time * speed, travelDistance) * (reverse ? -1 : 1);
        }

        transform.position = position;

        if (!alreadyAttacked)
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        Vector3 attackPosition = transform.position + attackPositionOffset;
        Collider[] colliders = Physics.OverlapSphere(attackPosition, 1.0f);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Player"))
            {
                HealthController playerHealthController = col.GetComponent<HealthController>();
                if (playerHealthController != null)
                {
                    playerHealthController.TakeDamage(damagePerAttack);
                    alreadyAttacked = true;
                    Invoke(nameof(ResetAttack), timeBetweenAttacks);
                    return;
                }
            }
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 gizmoWorldPosition = transform.position + attackPositionOffset;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gizmoWorldPosition, 1f);
    }
}
