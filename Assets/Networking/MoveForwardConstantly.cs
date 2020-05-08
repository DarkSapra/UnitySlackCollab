namespace USC.Networking
{
    using Photon.Pun;
    using UnityEngine;

    public class MoveForwardConstantly : MonoBehaviourPun, IRideable
    {
        private Transform thisTransform;
        private Vector3 previousPosition;
        private Vector3 moveVector;
        private bool isMoving = false;
        public bool IsMoving
        {
            get { return isMoving; }
            set { isMoving = value; }
        }

        public Vector3 MovementVector { get { return moveVector; } }

        private void Awake()
        {
            thisTransform = transform;
        }

        private void Update()
        {
            // if owner and moving, move ship
            if (PhotonNetwork.IsMasterClient)
            {
                if (!isMoving) return;
                thisTransform.Translate(Vector3.forward * Time.deltaTime);
            }
            // calculate distance moved
            moveVector = thisTransform.position - previousPosition;
            // save position for next frame
            previousPosition = thisTransform.position;
        }
    }
}