using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
     #region Input actions
     public InputSystem_Actions playerControls;
     private InputAction moveAction;
     private InputAction jumpAction;
     private InputAction rotateAction;
     private InputAction sprintAction;
     private InputAction fireAction;
     private InputAction interactAction;
     private InputAction reloadAction;

     // TODO: Look into making a UIController to separate out UI action inputs
     // TODO: Need a gamepad navigation for UI buttons
     private InputAction startButtonAction;
     #endregion

     public Camera playerCamera;
     public float walkSpeed = 6f;
     public float runSpeed = 12f;
     public float jumpPower = 8.0f;
     public float gravity = 20.0f;

     public float lookSpeed = 1f;
     public float lookXLimit = 45f;

     public bool isRunning;

     public UnityAction StartPressed;

     private CharacterController characterController;
     public ProjectileLauncher launcher;
     [SerializeField]
     private InteractionProbe interactionProbe;

     private Vector3 moveDirection = Vector3.zero;
     private Vector3 inputDirection = Vector3.zero;
     private Vector3 inputRotation = Vector3.zero;
     private float rotationX = 0;

     [HideInInspector]
     public bool canMove = true;
     [HideInInspector]
     public bool canFire = true;
     public bool canSprint = true;

     public bool disableControls = false;

     private void Awake()
     {
          playerControls = new InputSystem_Actions();
     }

     private void OnEnable()
     {
          moveAction = playerControls.Player.Move;
          moveAction.Enable();

          sprintAction = playerControls.Player.Sprint;
          sprintAction.Enable();

          jumpAction = playerControls.Player.Jump;
          jumpAction.Enable();
          jumpAction.performed += Jump;

          rotateAction = playerControls.Player.Look;
          rotateAction.Enable();

          fireAction = playerControls.Player.Attack;
          fireAction.Enable();
          fireAction.performed += Fire;

          interactAction = playerControls.Player.Interact;
          interactAction.Enable();
          interactAction.performed += Interact;

          reloadAction = playerControls.Player.Reload;
          reloadAction.Enable();
          reloadAction.performed += Reload;

          startButtonAction = playerControls.Player.Pause;
          startButtonAction.Enable();
          startButtonAction.performed += Pause;
     }

     private void OnDisable()
     {
          moveAction.Disable();
          jumpAction.Disable();
          rotateAction.Disable();
          sprintAction.Disable();
          fireAction.Disable();
          interactAction.Disable();
          reloadAction.Disable();
          startButtonAction.Disable();
     }

     void Start()
     { 
          characterController = GetComponent<CharacterController>();
          Cursor.lockState = CursorLockMode.Locked;
          Cursor.visible = false;
     }

     void Update()
     {
          HandleMovement();

          HandleRotation();
     }

     private void HandleMovement()
     {
          if(disableControls) { return; }

          inputDirection = moveAction.ReadValue<Vector2>();
          Vector3 forward = transform.TransformDirection(Vector3.forward);
          Vector3 right = transform.TransformDirection(Vector3.right);

          isRunning = sprintAction.IsPressed();
          float curSpeedX = canMove ? (isRunning && canSprint ? runSpeed : walkSpeed) * inputDirection.y : 0;
          float curSpeedY = canMove ? (isRunning && canSprint ? runSpeed : walkSpeed) * inputDirection.x : 0;
          float movementDirectionY = moveDirection.y;
          moveDirection = (forward * curSpeedX) + (right * curSpeedY);
          moveDirection.y = movementDirectionY;

          GravityTick();

          characterController.Move(moveDirection * Time.deltaTime);
     }

     private void HandleRotation()
     {
          if (disableControls) { return; }

          if (canMove)
          {
               inputRotation = rotateAction.ReadValue<Vector2>();
               rotationX += -inputRotation.y * lookSpeed;
               rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
               playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
               transform.rotation *= Quaternion.Euler(0, inputRotation.x * lookSpeed, 0);
          }
     }

     private void Jump(InputAction.CallbackContext context)
     {
          if (disableControls) { return; }

          if (canMove && characterController.isGrounded)
          {
               moveDirection.y = jumpPower;
          }
     }

     private void Fire(InputAction.CallbackContext context)
     {
          if (disableControls) { return; }

          if (canFire)
          {
               launcher.Launch();
          }
     }

     private void Interact(InputAction.CallbackContext context)
     {
          if (disableControls) { return; }

          interactionProbe.Interact();
     }

     private void Reload(InputAction.CallbackContext context)
     {
          if (disableControls) { return; }

          launcher.StartReloadIterative();
     }
     private void Pause(InputAction.CallbackContext context)
     {
          StartPressed?.Invoke();
     }

     private void GravityTick()
     {
          if (!characterController.isGrounded)
          {
               moveDirection.y -= gravity * Time.deltaTime;
          }
     }


}