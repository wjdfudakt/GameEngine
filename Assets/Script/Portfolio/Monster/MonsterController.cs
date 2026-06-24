using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float detectDistance = 10f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float stopDistance = 5f;
    [SerializeField] private float rotationSpeed = 5f;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
            target = player.transform;
    }

    private void Update()
    {
        if (target == null)
            return;

        Vector3 direction = target.position - transform.position;
        direction.y = 0f;

        float distance = direction.magnitude;

        if (distance > detectDistance)
            return;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        if (distance > stopDistance)
        {
            transform.position +=
                direction.normalized * moveSpeed * Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}