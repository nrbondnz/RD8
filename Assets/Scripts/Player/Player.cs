
using System;
using System.ComponentModel.Design.Serialization;
using Unity.VisualScripting;

namespace Player
{
    public class Player
    {
        private static Player Instance;
        private bool _isJumpButtonPressed = false;
        private bool _isGrounded = true;
        private bool _goingForwards = true;
        private float _hozInput;
        private float _vertInput;

        public static Player getInstance()
        {
            if (Instance.IsUnityNull())
            {
                Instance = new Player();
            }

            return Instance;
        }

        public void Awake()
        {
            getInstance();
        }
        
        public float HozInput
        {
            get => getInstance()._hozInput;
            set => getInstance()._hozInput = value;
        }

        public float VertInput
        {
            get => getInstance()._vertInput;
            set => getInstance()._vertInput = value;
        }

        public bool IsJumpButtonPressed
        {
            get => getInstance()._isJumpButtonPressed;
            set => getInstance()._isJumpButtonPressed = value;
        }

        public bool IsGrounded
        {
            get => getInstance()._isGrounded;
            set => getInstance()._isGrounded = value;
        }

        public bool GoingForwards
        {
            get => getInstance()._goingForwards;
            set => getInstance()._goingForwards = value;
        }

        public override string ToString()
        {
            return $"{nameof(_isJumpButtonPressed)}: {_isJumpButtonPressed}, {nameof(_isGrounded)}: {_isGrounded}, {nameof(_goingForwards)}: {_goingForwards}, {nameof(_hozInput)}: {_hozInput}, {nameof(_vertInput)}: {_vertInput}, {nameof(HozInput)}: {HozInput}, {nameof(VertInput)}: {VertInput}, {nameof(IsJumpButtonPressed)}: {IsJumpButtonPressed}, {nameof(IsGrounded)}: {IsGrounded}, {nameof(GoingForwards)}: {GoingForwards}";
        }
    }
}