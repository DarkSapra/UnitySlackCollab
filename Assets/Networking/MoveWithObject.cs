namespace USC.Networking
{
    using System.Collections.Generic;
    using UnityEngine;

    public class MoveWithObject : MonoBehaviour
    {
        public List<Rigidbody> rigidbodies = new List<Rigidbody>();
        private Vector3 previousPosition;
        private Transform thisTransform;

        private void Awake()
        {
            thisTransform = transform;
        }

        private void LateUpdate()
        {
            Vector3 moveVector = thisTransform.position - previousPosition;
            previousPosition = thisTransform.position;

            foreach (Rigidbody body in rigidbodies)
            {
                body.MovePosition(body.position + moveVector);
            }
        }

        public void AddBody(Rigidbody body)
        {
            if (!rigidbodies.Contains(body))
            {
                rigidbodies.Add(body);
            }
        }

        public void RemoveBody(Rigidbody body)
        {
            rigidbodies.Remove(body);
        }


    }
}