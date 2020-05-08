namespace USC.Networking
{
    using Photon.Pun;
    using System.Collections;
    using UnityEngine;

    public class JoinOrCreateRoom : MonoBehaviourPunCallbacks
    {
        [SerializeField] GameObject playerPrefab = default;
        private int joinAttempts = 0;

        private void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        private IEnumerator JoinRoomDelay()
        {
            yield return new WaitForSeconds(0.1f);
            PhotonNetwork.JoinRandomRoom();
        }

        private void CreateRoom()
        {
            PhotonNetwork.CreateRoom(null);
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.NickName = "Player_" + Random.Range(111111, 999999);
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            joinAttempts++;
            if(joinAttempts < 10)
            {
                StartCoroutine(JoinRoomDelay());
            }
            else
            {
                CreateRoom();
            }
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Joined Room: " + PhotonNetwork.CurrentRoom.Name);
            if (PhotonNetwork.IsMasterClient)
            {
                GameObject.Find("Ship").GetComponent<PhotonView>().RequestOwnership();
            }
            Vector3 spawnlocation = GameObject.Find("Ship").transform.position + Vector3.up * 5;
            PhotonNetwork.Instantiate(playerPrefab.name, spawnlocation, Quaternion.identity);
        }
    }
}