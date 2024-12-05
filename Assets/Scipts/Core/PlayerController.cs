using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Transform playerBody;
    public Camera playerCamera;

    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float crouchSpeed = 2.5f;
    public float jumpForce = 5f;

    [Header("Mouse Settings")]
    public float mouseSensitivity;
    private float xRotation = 0f;

    [Header("Game Settings")]
    private string selectedGame = "Valorant"; // Default game
    private float fov = 90f;

    private CharacterController characterController;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool isCrouching = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

        // Apply saved settings
        ApplySettings();
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
    }

    private void ApplySettings()
    {
        // Retrieve game settings
        selectedGame = PlayerPrefs.GetString("SelectedGame", "Valorant");
        fov = PlayerPrefs.GetFloat("FOV", 90f);

        // Set camera FOV based on saved settings
        AdjustFOV();

        // Adjust movement speeds and sensitivity based on the selected game
        AdjustGameSettings();
    }

    private void AdjustFOV()
    {
        playerCamera.fieldOfView = fov;
    }

    private void AdjustGameSettings()
    {
        // Adjust movement and sensitivity scale based on the game
        switch (selectedGame)
        {
            case "Valorant":
                walkSpeed = 3.75f;
                runSpeed = 5.5f;
                crouchSpeed = 1.88f;
                break;

            case "CS2":
                walkSpeed = 4.8f;
                runSpeed = 6.4f;
                crouchSpeed = 2.4f;
                break;

            default:
                Debug.LogWarning("Selected game not recognized. Using default values.");
                walkSpeed = 5f;
                runSpeed = 8f;
                crouchSpeed = 2.5f;
                break;
        }
    }

    private void HandleMouseLook()
    {
        // Adjust mouse sensitivity
        lookInput *= mouseSensitivity;

        // Rotate the player body based on the horizontal look input
        playerBody.Rotate(Vector3.up * lookInput.x);

        // Rotate the camera based on the vertical look input
        xRotation -= lookInput.y;

        // Clamp the vertical rotation of the camera
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply the rotation to the camera
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void HandleMovement()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        characterController.Move(move * walkSpeed * Time.deltaTime);

        // Apply gravity
        Vector3 gravity = Physics.gravity * Time.deltaTime;
        characterController.Move(gravity);
    }

    public void OnMove(Vector2 input)
    {
        moveInput = input;
    }

    public void OnLook(Vector2 input)
    {
        lookInput = input;
    }

    public void ApplyCrouch()
    {
        isCrouching = !isCrouching;
        walkSpeed = isCrouching ? crouchSpeed : walkSpeed;
    }
}
