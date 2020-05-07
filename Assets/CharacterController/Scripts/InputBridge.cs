namespace USC.CharacterController
{
    using UnityEngine;

    /// <summary>
    /// Component for checking player input
    /// </summary>
    public class InputBridge : MonoBehaviour
    {
        [SerializeField] private string horizontal = "Horizontal";
        [SerializeField] private string vertical = "Vertical";
        [SerializeField] private string mouseHorizontal = "Mouse X";
        [SerializeField] private string mouseVertical = "Mouse Y";
        [SerializeField] private KeyCode jumpKey = KeyCode.Space;

        private Vector2 moveAxis;
        private Vector2 lookAxis;
        private bool jumpButton;

        public Vector2 MoveAxis() { return moveAxis; }
        public Vector2 LookAxis() { return lookAxis; }
        public bool Jump() { return jumpButton; }

        private void Update()
        {
            UpdateInputs();
        }

        /// <summary>
        /// Check for player input
        /// </summary>
        private void UpdateInputs()
        {
            moveAxis = new Vector2
            (
                Input.GetAxis(horizontal),
                Input.GetAxis(vertical)
            );
            lookAxis = new Vector2
            (
                Input.GetAxis(mouseHorizontal),
                Input.GetAxis(mouseVertical)
            );
            jumpButton = Input.GetKey(jumpKey);
        }
    }
}