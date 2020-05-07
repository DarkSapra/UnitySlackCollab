namespace USC.CharacterController
{
    using UnityEngine;

    /// <summary>
    /// Moves the player avatar
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(InputBridge))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Controller Settings")]
        [SerializeField] float moveSpeed = 10f;
        [SerializeField] float jumpSpeed = 15f;
        [SerializeField] float gravity = 30f;
        [SerializeField] LayerMask groundLayers = default;

        [Header("Collider Settings")]
        [SerializeField] float colliderHeight = 2f;
        [SerializeField] float colliderRadius = 1f;
        [Range(0f, 1f)] [SerializeField] float stepHeight = 0.25f;
        [SerializeField] Vector3 colliderOffset = Vector3.zero;

        private Transform tr;
        private CapsuleCollider col;
        private Rigidbody rig;
        private float verticalSpeed = 0f;
        private float baseLength = 0f;
        private float castLength = 0f;
        private bool isGrounded = false;
        private Vector3 velocityAdjustment = Vector3.zero;
        private Vector3 origin = Vector3.zero;
        private InputBridge inputBridge;

        public enum CastDirection
        {
            Forward,
            Right,
            Up,
            Backward,
            Left,
            Down
        }
        private CastDirection castDirection;

        private void Awake()
        {
            tr = transform;
            inputBridge = GetComponent<InputBridge>();
            rig = GetComponent<Rigidbody>();
            col = GetComponent<CapsuleCollider>();
            rig.freezeRotation = true;
            rig.useGravity = false;
            SetupCollider();
            CalibrateRaycast();
        }

        /// <summary>
        /// This is used to show the updated Collider in the editor
        /// </summary>
        private void OnValidate()
        {
            if (gameObject.activeInHierarchy) SetupCollider();
        }

        private void FixedUpdate()
        {
            GroundCheck();
            Vector3 velocity = Vector3.zero;
            velocity += GetMoveDirection() * moveSpeed;
            if (!isGrounded)
            {
                verticalSpeed -= gravity * Time.deltaTime;
            }
            else
            {
                if (verticalSpeed <= 0f) verticalSpeed = 0f;
            }
            if (isGrounded && inputBridge.Jump())
            {
                verticalSpeed = jumpSpeed;
                isGrounded = false;
            }
            velocity += tr.up * verticalSpeed;
            SetVelocity(velocity);
        }

        /// <summary>
        /// Update if player is grounded
        /// </summary>
        private void GroundCheck()
        {
            velocityAdjustment = Vector3.zero;
            if (isGrounded)
            {
                castLength = baseLength + colliderHeight * stepHeight;
            }
            else
            {
                castLength = baseLength;
            }
            RaycastHit hit;
            if (Physics.Raycast(tr.TransformPoint(origin), GetCastDirection(), out hit, castLength, groundLayers, QueryTriggerInteraction.Ignore))
            {
                isGrounded = true;
                float upper = (colliderHeight * (1.0f - stepHeight)) * 0.5f;
                float middle = upper + colliderHeight * stepHeight;
                float distance = middle - hit.distance;
                velocityAdjustment = tr.up * (distance / Time.deltaTime);
            }
            else
            {
                isGrounded = false;
            }
        }

        /// <summary>
        /// Translate the enum into a transform direction
        /// </summary>
        private Vector3 GetCastDirection()
        {
            switch (castDirection)
            {
                case CastDirection.Forward:
                    return tr.forward;
                case CastDirection.Right:
                    return tr.right;
                case CastDirection.Up:
                    return tr.up;
                case CastDirection.Backward:
                    return -tr.forward;
                case CastDirection.Left:
                    return -tr.right;
                case CastDirection.Down:
                    return -tr.up;
                default:
                    return Vector3.one;
            }
        }

        /// <summary>
        /// Use input to calculate direction to move
        /// </summary>
        private Vector3 GetMoveDirection()
        {
            Vector3 direction = Vector3.zero;
            direction += Vector3.ProjectOnPlane(tr.right, tr.up).normalized * inputBridge.MoveAxis().x;
            direction += Vector3.ProjectOnPlane(tr.forward, tr.up).normalized * inputBridge.MoveAxis().y;
            direction.Normalize();
            return direction;
        }

        /// <summary>
        /// Set the attached collider based on the controller variables
        /// </summary>
        private void SetupCollider()
        {
            if (col == null) col = GetComponent<CapsuleCollider>();
            col.height = colliderHeight;
            col.center = colliderOffset * colliderHeight;
            col.radius = colliderRadius * 0.5f;
            col.center = col.center + new Vector3(0.0f, (stepHeight * col.height) * 0.5f, 0.0f);
            col.height *= (1.0f - stepHeight);
        }

        /// <summary>
        /// Set the base raycast variables
        /// </summary>
        private void CalibrateRaycast()
        {
            origin = tr.InverseTransformPoint(col.bounds.center);
            castDirection = CastDirection.Down;
            float length = 0.0f;
            length += (colliderHeight * (1.0f - stepHeight)) * 0.5f;
            length += colliderHeight * stepHeight;
            baseLength = length * 1.001f;
        }

        /// <summary>
        /// Set the velocity of the attached rigidbody
        /// </summary>
        public void SetVelocity(Vector3 velocity)
        {
            rig.velocity = velocity + velocityAdjustment;
        }
    }
}