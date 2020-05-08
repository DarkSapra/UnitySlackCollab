namespace USC.Networking
{
    using Photon.Pun;
    using UnityEngine;
    using USC.CharacterController;

    public class AvatarNetworkSetup : MonoBehaviourPun
    {
        [SerializeField] GameObject cameraRoot;
        private PhotonView view;

        private void Awake()
        {
            view = GetComponent<PhotonView>();
            if (!view.IsMine)
            {
                DisableNetworkPlayer();
            }
        }

        private void DisableNetworkPlayer()
        {
            // turn off camera, input, movement
            Destroy(cameraRoot);
            GetComponent<InputBridge>().enabled = false;
            GetComponent<PlayerController>().enabled = false;
            GetComponent<PlayerRotate>().enabled = false;
            GetComponent<RideObject>().enabled = false;
            GetComponent<ControlShip>().enabled = false;
        }
    }
}