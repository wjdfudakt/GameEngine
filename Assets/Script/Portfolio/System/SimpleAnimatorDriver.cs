using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SimpleAnimatorDriver : MonoBehaviour
{
    private static readonly int SpeedHash = Animator.StringToHash("Speed");

    private Animator animator;
    private CharacterController characterController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponentInParent<CharacterController>();
    }

    private void Update()
    {
        Vector3 horizontalVelocity = characterController.velocity;
        horizontalVelocity.y = 0f;

        float speed = horizontalVelocity.magnitude;
        animator.SetFloat(SpeedHash, speed, 0.1f, Time.deltaTime);
    }
}