using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class SimpleCharacterControllerMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float rotationSpeed = 120f;
    [SerializeField] private float gravity = -9.81f;

    private CharacterController controller;
    private InputAction moveAction;
    private InputAction rotateAction;
    private float verticalVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        moveAction = new InputAction("Move", InputActionType.Value);
        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");

        rotateAction = new InputAction("Rotate", InputActionType.Value);
        rotateAction.AddCompositeBinding("1DAxis")
            .With("Negative", "<Keyboard>/leftArrow")
            .With("Positive", "<Keyboard>/rightArrow");
    }

    private void OnEnable()
    {
        moveAction.Enable();
        rotateAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        rotateAction.Disable();
    }

    private void Update()
    {
        float rotateInput = rotateAction.ReadValue<float>();
        transform.Rotate(
            0f,
            rotateInput * rotationSpeed * Time.deltaTime,
            0f
        );

        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        Vector3 move =
            transform.right * moveInput.x +
            transform.forward * moveInput.y;

        move = move.normalized * moveSpeed;

        if (controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -1f;
        }

        verticalVelocity += gravity * Time.deltaTime;
        move.y = verticalVelocity;

        controller.Move(move * Time.deltaTime);
    }
}