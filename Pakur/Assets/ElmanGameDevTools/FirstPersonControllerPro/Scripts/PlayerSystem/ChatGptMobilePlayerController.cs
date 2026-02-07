using UnityEngine;

namespace ElmanGameDevTools.PlayerSystem
{
    [RequireComponent(typeof(CharacterController))]
    public class ChatGptMobilePlayerController : MonoBehaviour
    {
        [Header("REFERENCES")]
        public CharacterController controller;
        public Transform playerCamera;
        public MobileInput mobileInput;

        [Header("MOVEMENT")]
        public float speed = 6f;
        public float runSpeed = 9f;
        public float jumpHeight = 1.2f;
        public float gravity = -25f;

        [Header("LOOK")]
        public float sensitivity = 2f;
        public float touchLookSensitivity = 0.12f;
        public float maxLookUp = 90f;
        public float maxLookDown = -90f;

        [Header("CROUCH")]
        public float crouchHeight = 1.2f;
        public float crouchSmooth = 8f;

        [Header("HEADBOB")]
        public bool headBob = true;
        public float bobX = 0.04f;
        public float bobY = 0.05f;
        public float bobSpeed = 12f;

        private Vector3 velocity;
        private float originalHeight;
        private float cameraBaseY;
        private float yaw;
        private float pitch;
        private float bobTimer;

        private bool isGrounded;
        private bool isCrouching;

        [HideInInspector] public bool jumpPressed;
        [HideInInspector] public bool runHeld;
        [HideInInspector] public bool crouchHeld;

        void Start()
        {
            if (!controller) controller = GetComponent<CharacterController>();
            originalHeight = controller.height;
            cameraBaseY = playerCamera.localPosition.y;

            yaw = transform.eulerAngles.y;
            pitch = playerCamera.localEulerAngles.x;
        }

        void Update()
        {
            CheckGround();
            HandleMovement();
            HandleLook();
            HandleCrouch();
            if (headBob) HandleHeadBob();
        }

        void CheckGround()
        {
            isGrounded = controller.isGrounded;
            if (isGrounded && velocity.y < 0)
                velocity.y = -5f;
        }

        void HandleMovement()
        {
            Vector3 move =
                transform.right * mobileInput.Move.x +
                transform.forward * mobileInput.Move.y;

            float currentSpeed = runHeld ? runSpeed : speed;
            controller.Move(move * currentSpeed * Time.deltaTime);

            if (jumpPressed && isGrounded && !isCrouching)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            jumpPressed = false;

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        void HandleLook()
        {
            yaw += mobileInput.Look.x * sensitivity * touchLookSensitivity;
            pitch -= mobileInput.Look.y * sensitivity * touchLookSensitivity;
            pitch = Mathf.Clamp(pitch, maxLookDown, maxLookUp);

            transform.rotation = Quaternion.Euler(0, yaw, 0);
            playerCamera.localRotation = Quaternion.Euler(pitch, 0, 0);
        }

        void HandleCrouch()
        {
            isCrouching = crouchHeld;
            float targetHeight = isCrouching ? crouchHeight : originalHeight;
            controller.height = Mathf.Lerp(controller.height, targetHeight, Time.deltaTime * crouchSmooth);

            Vector3 camPos = playerCamera.localPosition;
            camPos.y = Mathf.Lerp(camPos.y, cameraBaseY * (controller.height / originalHeight), Time.deltaTime * crouchSmooth);
            playerCamera.localPosition = camPos;
        }

        void HandleHeadBob()
        {
            if (!isGrounded || mobileInput.Move.magnitude < 0.1f)
            {
                bobTimer = 0;
                return;
            }

            bobTimer += Time.deltaTime * bobSpeed;
            playerCamera.localPosition += new Vector3(
                Mathf.Cos(bobTimer) * bobX,
                Mathf.Sin(bobTimer * 2f) * bobY,
                0
            );
        }

        // UI BUTTONS
        public void JumpButton() => jumpPressed = true;
        public void RunButton(bool v) => runHeld = v;
        public void CrouchButton(bool v) => crouchHeld = v;
    }
}
