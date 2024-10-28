using Unity.Jobs.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
     public InputSystem_Actions playerControls;
     private InputAction moveAction;
     private InputAction jumpAction;


     public Camera playerCamera;
     public float walkSpeed = 6f;
     public float runSpeed = 12f;
     public float jumpPower = 7f;
     public float gravity = 10f;


     public float lookSpeed = 2f;
     public float lookXLimit = 45f;


     Vector3 moveDirection = Vector3.zero;
     Vector3 inputDirection = Vector3.zero;
     float rotationX = 0;

     public bool canMove = true;

     private void Awake()
     {
          playerControls = new InputSystem_Actions();
     }

     private void OnEnable()
     {
          moveAction = playerControls.Player.Move;
          moveAction.Enable();

          jumpAction = playerControls.Player.Jump;
          jumpAction.Enable();
          jumpAction.performed += Jump;
     }

     private void OnDisable()
     {
          moveAction.Disable();
          jumpAction.Disable();
     }

     CharacterController characterController;
     void Start()
     { 
          characterController = GetComponent<CharacterController>();
          Cursor.lockState = CursorLockMode.Locked;
          Cursor.visible = false;
     }

     void Update()
     {

          #region Handles Movement
          inputDirection = moveAction.ReadValue<Vector2>();
          Vector3 forward = transform.TransformDirection(Vector3.forward);
          Vector3 right = transform.TransformDirection(Vector3.right);

          // Press Left Shift to run
          bool isRunning = Input.GetKey(KeyCode.LeftShift);
          float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * inputDirection.y : 0;
          float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * inputDirection.x : 0;
          float movementDirectionY = moveDirection.y;
          moveDirection = (forward * curSpeedX) + (right * curSpeedY);
          moveDirection.y = movementDirectionY;

          #endregion

          moveDirection.y = movementDirectionY;
          if (!characterController.isGrounded)
          {
               moveDirection.y -= gravity * Time.deltaTime;
          }

          #region Handles Rotation
          characterController.Move(moveDirection * Time.deltaTime);

          if (canMove)
          {
               rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
               rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
               playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
               transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
          }

          #endregion
     }
     private void Jump(InputAction.CallbackContext context)
     {
          if (canMove && characterController.isGrounded)
          {
               moveDirection.y = jumpPower;
          }
     }
}