using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 120f;

    private CharacterController controller;

    private Vector2 moveInput;

    private float lookInput;

    private PlayerCombat combat;
    private bool autoMove;

    public bool IsMoving => moveInput.sqrMagnitude > 0.01f;
        
    public bool IsAutoMoving => autoMove;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        combat = GetComponent<PlayerCombat>();
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

        if (moveInput.sqrMagnitude > 0.01f)
            autoMove = false;
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<float>();
    }

    public void StartAutoMove()
    {
        if (combat == null || combat.CurrentTarget == null)
            return;

        autoMove = true;
    }

    public void OnAutoMove(InputValue value)
    {
        if (!value.isPressed)
            return;

        StartAutoMove();
    }

    public void StopAutoMove()
    {
        autoMove = false;
    }

    private void Update()
    {
        transform.Rotate(
        Vector3.up,
        lookInput * rotateSpeed * Time.deltaTime);

        Vector3 move = Vector3.zero;

        if (autoMove)
        {
            if (combat.CurrentTarget == null)
            {
                autoMove = false;
            }
            else
            {
                Vector3 direction =
                    combat.CurrentTarget.position - transform.position;

                direction.y = 0f;

                float distance = direction.magnitude;

                PlayerClass playerClass = GetComponent<PlayerClass>();

                if (distance <= playerClass.AttackRange)
                {
                    autoMove = false;
                }
                else
                {
                    direction.Normalize();

                    transform.forward = direction;
                    move = direction;
                }
            }
        }
        else
        {
            move =
                transform.forward * moveInput.y +
                transform.right * moveInput.x;
        }

        controller.Move(
            move * moveSpeed * Time.deltaTime);
    }
}