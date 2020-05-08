namespace USC.Networking
{
    using Photon.Pun;
    using UnityEngine;

    public class ControlShip : MonoBehaviourPun
    {
        private MoveForwardConstantly moveable;

        private void Awake()
        {
            moveable = GameObject.Find("Ship")
                .GetComponent<MoveForwardConstantly>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (!PhotonNetwork.IsMasterClient) return;
                moveable.IsMoving = !moveable.IsMoving;
                Debug.Log(moveable.IsMoving);
            }
        }
    }
}