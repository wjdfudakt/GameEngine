using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float detectDistance = 20f;
    [SerializeField] private float attackDistance = 5f;

    private readonly List<Transform> detectedTargets = new();
    private Transform currentTarget;

    private void Update()
    {
        FindTargets();
        SelectNearestTarget();

        if (currentTarget == null)
            return;

        float distance = Vector3.Distance(
            transform.position,
            currentTarget.position);

        if (distance <= attackDistance)
        {
            Debug.Log($"공격! : {currentTarget.name}");
        }
    }

    private void FindTargets()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

        foreach (GameObject monster in monsters)
        {
            float distance = Vector3.Distance(
                transform.position,
                monster.transform.position);

            bool isDetected = detectedTargets.Contains(monster.transform);

            if (distance <= detectDistance && !isDetected)
            {
                detectedTargets.Add(monster.transform);
                Debug.Log($"{monster.name} 발견!");
            }

            if (distance > detectDistance && isDetected)
            {
                detectedTargets.Remove(monster.transform);
                Debug.Log($"{monster.name} 놓침!");
            }
        }

        detectedTargets.RemoveAll(t => t == null);
    }

    private void SelectNearestTarget()
    {
        currentTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform target in detectedTargets)
        {
            float distance = Vector3.Distance(
                transform.position,
                target.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                currentTarget = target;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}