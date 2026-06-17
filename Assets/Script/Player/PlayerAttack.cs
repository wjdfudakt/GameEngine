using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private float detectRange = 10f;
    [SerializeField]
    private float attackRange = 3f;


    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectRange);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Monster"))
            {
                Debug.Log("몬스터 감지: " + hit.name);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.orange;
        Gizmos.DrawWireSphere(transform.position, detectRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
