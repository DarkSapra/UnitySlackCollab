namespace USC.CharacterController
{
    using UnityEngine;

    /// <summary>
    /// Rotate the player avatar based on input
    /// </summary>
    public class PlayerRotate : MonoBehaviour
    {
        private Transform transformToRotate;
        private float currentRotation = 0f;
        private InputBridge inputBridge;

        private void Awake()
        {
            transformToRotate = transform;
            currentRotation =
                transformToRotate.localRotation.eulerAngles.y;
            inputBridge = GetComponent<InputBridge>();
        }

        private void FixedUpdate()
        {
            ProcessRotation();
        }

        /// <summary>
        /// Rotate transform based on input value from InputBridge
        /// </summary>
        private void ProcessRotation()
        {
            currentRotation += 
                inputBridge.LookAxis().x * 
                Time.deltaTime * 10;
            transformToRotate.localRotation = 
                Quaternion.Euler(Vector3.up * currentRotation);
        }
    }
}