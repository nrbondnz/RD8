
using System;
using Unity.VisualScripting;
using UnityEngine;
using Utilities;

namespace Player
{
    /// <summary>
    /// Player records input states and player action stage for update in PlayerMovement
    /// </summary>
    public class OnScreenPlayerUpdate
    {
        private static OnScreenPlayerUpdate _instance;
        private bool _isJumpButtonPressed;
        private bool _isGrounded = true;
        private bool _goingForwards = true;
        private float _hozInput;
        private float _vertInput;

        public static OnScreenPlayerUpdate GetInstance()
        {
            if (_instance.IsUnityNull())
            {
                _instance = new OnScreenPlayerUpdate();
            }

            return _instance;
        }

        public void Awake()
        {
            GetInstance();
            //Actions.OnPlayerChanged += UpdatePlayer;
        }


        private void UpdatePlayer(OnScreenPlayerUpdate pOnScreenPlayerUpdate)
        {
            //_player = pPlayer;
            this.VertInput = pOnScreenPlayerUpdate.VertInput;
            this.HozInput = pOnScreenPlayerUpdate.HozInput;
            Debug.Log("Player x : " + this.HozInput + ", y " + this.VertInput);
        }
        
        
        public float HozInput
        {
            get => GetInstance()._hozInput;
            set => GetInstance()._hozInput = value;
        }

        public float VertInput
        {
            get => GetInstance()._vertInput;
            set => GetInstance()._vertInput = value;
        }

        public bool IsJumpButtonPressed
        {
            get => GetInstance()._isJumpButtonPressed;
            set => GetInstance()._isJumpButtonPressed = value;
        }

        public bool IsGrounded
        {
            get => GetInstance()._isGrounded;
            set => GetInstance()._isGrounded = value;
        }

        public bool GoingForwards
        {
            get => GetInstance()._goingForwards;
            set => GetInstance()._goingForwards = value;
        }

        public override string ToString()
        {
            return $"{nameof(_isJumpButtonPressed)}: {_isJumpButtonPressed}, {nameof(_isGrounded)}: {_isGrounded}, {nameof(_goingForwards)}: {_goingForwards}, {nameof(_hozInput)}: {_hozInput}, {nameof(_vertInput)}: {_vertInput}, {nameof(HozInput)}: {HozInput}, {nameof(VertInput)}: {VertInput}, {nameof(IsJumpButtonPressed)}: {IsJumpButtonPressed}, {nameof(IsGrounded)}: {IsGrounded}, {nameof(GoingForwards)}: {GoingForwards}";
        }
    }
}