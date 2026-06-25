using UnityEngine;
using UnityEngine.UIElements;

public class MonsterAction : MonoBehaviour
{
    [SerializeField] private float detectRange = 10f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float moveSpeed = 3f;

    [SerializeField] private int damage = 5;
    [SerializeField] private float attackCooldown = 1f;
        
    [SerializeField] private Transform player;
    [SerializeField] private float attackTimer;

    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
        if (health != null)
        {
            health.OnDeath.AddListener(OnDeath);
        }
    }

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            this.player = player.transform;
        }
    }

    private void Update()
    {
        if (player == null)
            return;

        float distance = Vector3.Distance(transform.position, player.position);
        

        attackTimer += Time.deltaTime;

        if (distance <= detectRange)
        {
            if (distance > attackRange)
            {
                Vector3 direction = (player.position - transform.position) .normalized;
                direction.y = 0f;

                transform.position += direction * moveSpeed * Time.deltaTime;

                transform.forward = direction;
            }

            else
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        if (attackTimer < attackCooldown)
            return;

        attackTimer = 0f;

        Health playerHealth = player.GetComponent<Health>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    private void OnDeath()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}