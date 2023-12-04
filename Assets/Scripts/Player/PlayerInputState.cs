namespace Player
{
    public class PlayerInputState
    {
        private float _Horizontal;
        private float _Vertical;
        private bool jumpPressed;
        private bool grounded;

        public PlayerInputState(float horizontal, float vertical, bool jumpPressed = false, bool pGrounded = true)
        {
            _Horizontal = horizontal;
            _Vertical = vertical;
            this.jumpPressed = jumpPressed;
            this.grounded = pGrounded;
        }

        public float Horizontal => _Horizontal;

        public float Vertical => _Vertical;

        public bool JumpPressed
        {
            get => jumpPressed;
            set => jumpPressed = value;
        }

        public bool Grounded
        {
            get => grounded;
            set => grounded = value;
        }
    }
}