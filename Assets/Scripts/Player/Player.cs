
namespace Player
{
    public class Player
    {
        private bool _isJumpButtonPressed = false;
        private bool _isGrounded = true;
        private bool _goingForwards = true;
        private float _hozInput;
        private float _vertInput;

        public float HozInput
        {
            get => _hozInput;
            set => _hozInput = value;
        }

        public float VertInput
        {
            get => _vertInput;
            set => _vertInput = value;
        }

        public bool IsJumpButtonPressed
        {
            get => _isJumpButtonPressed;
            set => _isJumpButtonPressed = value;
        }

        public bool IsGrounded
        {
            get => _isGrounded;
            set => _isGrounded = value;
        }

        public bool GoingForwards
        {
            get => _goingForwards;
            set => _goingForwards = value;
        }
    }
}