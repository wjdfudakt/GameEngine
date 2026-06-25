using System.Collections.Generic;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour
{
    [SerializeField] private float detectRange = 10f;

    [SerializeField] public Transform CurrentTarget;

    private List<Transform> targets = new();

    private void Update()
    {
        FindTargets();

        if (CurrentTarget == null)
        {
            SelectNearestTarget();
        }
    }

    private void FindTargets()
    {
        targets.Clear();

        Collider[] hits = Physics.OverlapSphere(transform.position, detectRange);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Monster"))
            {
                targets.Add(hit.transform);
            }
        }
    }

    private void SelectNearestTarget()
    {
        float nearestDistance = float.MaxValue;

        foreach (Transform target in targets)
        {
            float distance =
                Vector3.Distance(transform.position, target.position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                CurrentTarget = target;
            }
        }
    }

    public void SelectNextTarget()
    {
        ChangeTarget(1);
    }

    public void SelectPreviousTarget()
    {
        ChangeTarget(-1);
    }

    private void ChangeTarget(int direction)
    {
        if (targets.Count == 0)
            return;

        int index = targets.IndexOf(CurrentTarget);

        if (index < 0)
            index = 0;

        index = (index + direction + targets.Count)
                % targets.Count;

        CurrentTarget = targets[index];
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}