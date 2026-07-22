using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SimpleAnimatorDriver : MonoBehaviour
{
    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int MoveXHash = Animator.StringToHash("MoveX");
    private static readonly int MoveYHash = Animator.StringToHash("MoveY");

    private Animator animator;
    private PlayerController controller;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponentInParent<PlayerController>();
    }

    private void Update()
    {
        Vector3 move = controller.AnimatorMove;

        animator.SetFloat(SpeedHash, move.sqrMagnitude);

        animator.SetFloat(
            MoveXHash,
            move.x,
            0.1f,
            Time.deltaTime);

        animator.SetFloat(
            MoveYHash,
            move.z,
            0.1f,
            Time.deltaTime);
    }
}