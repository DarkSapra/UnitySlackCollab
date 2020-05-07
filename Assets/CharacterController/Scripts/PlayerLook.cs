namespace USC.CharacterController
{
    using UnityEngine;

    /// <summary>
    /// Rotate the camera transform based on input
    /// </summary>
    public class PlayerLook : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 10f;
        [Range(0f, 90f)]
        [SerializeField] private float upperLimit = 60f;
        [Range(0f, 90f)]
        [SerializeField] private float lowerLimit = 60f;
        [SerializeField] private bool invert = false;

        private Transform transformToRotate;
        private float currentRotation = 0f;
        private InputBridge inputBridge;

        private void Awake()
        {
            transformToRotate = transform;
            currentRotation =
                transformToRotate.localRotation.eulerAngles.x;
            inputBridge = GetComponentInParent<InputBridge>();
        }

        private void Update()
        {
            ProcessRotation();
        }

        /// <summary>
        /// Rotate transform based on input value from InputBridge
        /// </summary>
        private void ProcessRotation()
        {
            if (invert)
            {
                currentRotation += 
                    inputBridge.LookAxis().y * 
                    Time.deltaTime * 
                    rotationSpeed * 10;
            }
            else
            {
                currentRotation -= 
                    inputBridge.LookAxis().y * 
                    Time.deltaTime * 
                    rotationSpeed * 10;
            }
            currentRotation = 
                Mathf.Clamp(currentRotation, -upperLimit, lowerLimit);
            transformToRotate.localRotation = 
                Quaternion.Euler(Vector3.right * currentRotation);
        }
    }
}