using UnityEngine;

public class WalkableAreaPreview : MonoBehaviour
{
    [SerializeField] private Vector2Int gridSize = new Vector2Int(7, 7);
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private float checkRadius = 0.35f;
    [SerializeField] private LayerMask obstacleMask;

    void OnDrawGizmos()
    {
        Vector3 start = transform.position;

        for (int z = 0; z < gridSize.y; z++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                Vector3 offset = new Vector3(
                    (x - gridSize.x / 2) * cellSize,
                    0f,
                    (z - gridSize.y / 2) * cellSize
                );

                Vector3 point = start + offset;
                bool blocked = Physics.CheckSphere(point, checkRadius, obstacleMask);

                Gizmos.color = blocked ? Color.red : Color.green;
                Gizmos.DrawWireSphere(point, checkRadius);
            }
        }
    }
}