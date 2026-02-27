using KBCore.Refs;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerInput : MonoBehaviour
{
    private InputAction move;
    private InputAction look;
    private InputAction jump;

    [SerializeField] private float maxSpeed = 10.0f;
    [SerializeField] private float gravity = -30.0f;

    private Vector3 velocity;

    [SerializeField] private float rotationSpeed = 4.0f;
    [SerializeField] private float mouseSensy = 5.0f;

    private float camXRotation;

    [SerializeField] private CharacterController controller;
    [SerializeField] private Camera cam;
    [SerializeField] private AudioController audioController;

    private void OnValidate()
    {
        this.ValidateRefs();
    }

    void Start()
    {
        move = InputSystem.actions.FindAction("Player/Move");
        look = InputSystem.actions.FindAction("Player/Look");
        jump = InputSystem.actions.FindAction("Player/Jump");

        jump.started += Jump;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        jump.started -= Jump;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        audioController.PlayJumpSFX();
    }

    void Update()
    {
        Vector2 readMove = move.ReadValue<Vector2>();
        Vector2 readLook = look.ReadValue<Vector2>(); // (0,0)

        // Movement of the player
        Vector3 movement = transform.right * readMove.x + transform.forward * readMove.y;

        velocity.y += gravity * Time.deltaTime;

        movement *= maxSpeed * Time.deltaTime;
        movement += velocity;

        controller.Move(movement);
    }
}