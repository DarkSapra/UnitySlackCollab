namespace USC.Networking
{
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class RideObject : MonoBehaviour
    {
        private MoveForwardConstantly rideable;
        private Rigidbody body;

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            rideable = GameObject.Find("Ship").GetComponent<MoveForwardConstantly>();
        }

        private void LateUpdate()
        {
            body.MovePosition(body.position + rideable.MovementVector);
        }
    }
}