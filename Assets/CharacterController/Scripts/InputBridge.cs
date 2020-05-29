namespace USC.CharacterController
{
    using UnityEngine;
    using Mirror;

    /// <summary>
    /// Component for checking player input
    /// </summary>
    public class InputBridge : NetworkBehaviour
    {
        [SerializeField] private string horizontal = "Horizontal";
        [SerializeField] private string vertical = "Vertical";
        [SerializeField] private string mouseHorizontal = "Mouse X";
        [SerializeField] private string mouseVertical = "Mouse Y";
        [SerializeField] private KeyCode jumpKey = KeyCode.Space;
        [SerializeField] private KeyCode shootKey = KeyCode.Mouse1;
        [SerializeField] private KeyCode interactKey = KeyCode.E;



        private Vector2 moveAxis;
        private Vector2 lookAxis;
        private bool jumpButton;
        private bool shootButton;
        private bool interactButton;

        public float verticalSens;
        public float horizontalSens;
        public Vector2 MoveAxis() { return moveAxis; }
        public Vector2 LookAxis() { return lookAxis; }
        public bool Jump() { return jumpButton; }
        public bool Shoot() { return shootButton; }
        public bool Interact() { return interactButton; }



        private void Update()
        {
            if(!isLocalPlayer)
                return;
            UpdateInputs();
        }

        /// <summary>
        /// Check for player input
        /// </summary>
        private void UpdateInputs()
        {
            moveAxis = new Vector2
            (
                Input.GetAxisRaw(horizontal),
                Input.GetAxisRaw(vertical)
            );
            lookAxis = new Vector2
            (
                Input.GetAxis(mouseHorizontal)*horizontalSens,
                Input.GetAxis(mouseVertical)*verticalSens
            );
            jumpButton = Input.GetKey(jumpKey);
            shootButton = Input.GetKey(shootKey);
            interactButton = Input.GetKey(interactKey);

        }
    }
}