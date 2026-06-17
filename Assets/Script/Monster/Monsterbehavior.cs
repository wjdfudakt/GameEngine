using UnityEngine;

public class Monsterbehavior : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float moveSpeed = 2f;
    [SerializeField]
    [Tooltip("몬스터의 공격 시도 거리")]
    private float stopDistance = 1.5f;//공격 사거리(추후 추가)

    void Update()
    {
        if (player == null) return;

        Vector3 direction = player.position - transform.position;
        float distance = direction.magnitude;

        if (distance <= stopDistance) return;

        direction.Normalize();

        transform.position += direction * moveSpeed * Time.deltaTime;
        
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
