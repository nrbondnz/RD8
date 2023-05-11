
using System;
using UnityEngine;

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

        public Vector2 getNormalisedHozVert()
        {
            return new Vector2(this._vertInput, this._hozInput).normalized;
        }

        public override string ToString()
        {
            return $"{nameof(_isJumpButtonPressed)}: {_isJumpButtonPressed}, {nameof(_isGrounded)}: {_isGrounded}, {nameof(_goingForwards)}: {_goingForwards}, {nameof(_hozInput)}: {_hozInput}, {nameof(_vertInput)}: {_vertInput}, {nameof(HozInput)}: {HozInput}, {nameof(VertInput)}: {VertInput}, {nameof(IsJumpButtonPressed)}: {IsJumpButtonPressed}, {nameof(IsGrounded)}: {IsGrounded}, {nameof(GoingForwards)}: {GoingForwards}";
        }
    }
}