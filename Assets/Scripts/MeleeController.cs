using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : AttackController
{
    [SerializeField]
    Transform attackPoint;

    [SerializeField]
    float damage;

    Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public override void Attack()
    {
        _animator.SetTrigger("attack");
    }

    public void OnAttack()
    {
        Collider[] others = Physics.OverlapSphere(attackPoint.position, 0.3F);
        foreach (Collider other in others)
        {
            HealthController controller = other.GetComponent<HealthController>();
            if (controller == null )
            {
                continue;
            }
            controller.TakeDamage(damage);
        }
    }
}
