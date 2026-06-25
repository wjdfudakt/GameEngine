using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private int damage = 10;

    private float timer;

    private PlayerTargeting targeting;

    private void Awake()
    {
        targeting = GetComponent<PlayerTargeting>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        Transform target = targeting.CurrentTarget;

        if (target == null)
            return;

        float distance =
            Vector3.Distance(transform.position, target.position);

        if (distance <= attackRange && timer >= attackCooldown)
        {
            Attack(target);
            timer = 0f;
        }
    }

    private void Attack(Transform target)
    {
        Health health = target.GetComponent<Health>();

        if (health != null)
        {
            health.TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}