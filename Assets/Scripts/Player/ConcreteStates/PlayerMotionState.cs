    using Unity.VisualScripting;
    using UnityEngine;

    public class PlayerMotionState : BaseState
    {  
        private PlayerController _player;
        private SoundEmitter _footstepEmitter;

        public PlayerMotionState(PlayerController player) : base(player)
        {
            _player = player;
        }

        public override void OnEnter() {}
        public override void OnExit() {
            _player.RB.linearVelocity = Vector2.zero;
            StopFootsteps();
        }


        public override void FixedUpdate()
        {
            Move();
            HandleFootsteps();
            
            if (_player.RB.linearVelocity.y < 0)
            {
                _player.RB.linearVelocity += Vector3.up * Physics.gravity.y * (_player.FallMultiplier - 1) * Time.fixedDeltaTime;
            }
        }

        void Move()
        {
            Vector2 input = _player.IsMovementBlocked? Vector2.zero :_player.Input.MoveDirection;

            // Get directions relative to player rotation
            Vector3 forward = _player.transform.forward;
            Vector3 right = _player.transform.right;

            // Keep movement flat
            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            Vector3 moveDir = forward * input.y + right * input.x;

            _player.RB.linearVelocity = new Vector3(moveDir.x * _player.MoveSpeed,_player.RB.linearVelocity.y,moveDir.z * _player.MoveSpeed);
        }

        void HandleFootsteps()
        {
            bool isMoving = _player.Input.MoveDirection != Vector2.zero && !_player.IsMovementBlocked;

            if (isMoving && _footstepEmitter == null)
            {
                StartFootsteps();
            }
            else if (!isMoving && _footstepEmitter != null)
            {
                StopFootsteps();
            }
        }

        void StartFootsteps()
        {
            var footstepData = SoundManager.Instance.SoundLibrary.Get("SFX_Footstep_Player");
            if (footstepData == null) return;

            _footstepEmitter = SoundManager.Instance.CreateSound()
                .WithSound(footstepData)
                .WithParent(_player.transform)
                .WithRandomPitch(false)
                .Play();
        }

        void StopFootsteps()
        {
            _footstepEmitter?.Stop();
            _footstepEmitter = null;
        }


        // void Jump()
        // {
        //     _player.RB.linearVelocity= new Vector3(_player.RB.linearVelocity.x, _player.JumpForce, _player.RB.linearVelocity.z);
        // }
    }
