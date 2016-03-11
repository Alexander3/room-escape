using Assets;
using Scripts;
using UnityEngine;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (CharacterController))]
    [RequireComponent(typeof (AudioSource))]
    public class FirstPersonController : MonoBehaviour
    {
		#region Serialized Fields
        [SerializeField] private bool _IsSneaking;
        [SerializeField] private float _SneakSpeed;
        [SerializeField] private float _RunSpeed;
        [SerializeField] [Range(0f, 1f)] private float _SneakHeightModifier;
        [SerializeField] [Range(0f, 10f)] private float _SneakHeightLerpSpeed;
        [SerializeField] [Range(0f, 1f)] private float _RunstepLenghten;
        [SerializeField] private float _JumpHeight;
        [SerializeField] private float _StickToGroundForce;
        [SerializeField] private float _GravityMultiplier;
        [SerializeField] private MouseLook _MouseLook;
        [SerializeField] private bool _UseFovKick;
        [SerializeField] private FOVKick _FovKick = new FOVKick();
        [SerializeField] private bool _UseHeadBob;
        [SerializeField] private CurveControlledBob _HeadBob = new CurveControlledBob();
        [SerializeField] private LerpControlledBob _JumpBob = new LerpControlledBob();
        [SerializeField] private float _StepInterval;
        [SerializeField] private AudioClip[] _MoveSounds;
        [SerializeField] [Range(0f,1f)] private float _SneakSoundVolumeScale;
        [SerializeField] private AudioClip[] _SneakingSounds;
        [SerializeField] private AudioClip _JumpSound;           // the sound played when character leaves the ground.
        [SerializeField] private AudioClip _LandSound;           // the sound played when character touches back on ground.
		#endregion
		#region Private Variables
        private Camera _Camera;
        private bool _Jump;
        private float _CharacterHeight;
        private Vector2 _Input;
        private Vector3 _MoveDir = Vector3.zero;
        private CharacterController _CharacterController;
        private CollisionFlags _CollisionFlags;
        private bool _PreviouslyGrounded = true;
        private Vector3 _OriginalCameraPosition;
        private float _StepCycle;
        private float _NextStep;
        private bool _Jumping;
        private AudioSource _AudioSource;
        private IControls _Controls;
        private Animator _Animator;
		#endregion	
		#region Properties
        public float MovingSpeed { get { return IsSneaking ? _SneakSpeed : _RunSpeed; } }
		public bool  IsNextStep  { get {  return (_StepCycle > _NextStep); } }
		public bool  IsCharacterGrounded  { get {  return _CharacterController.isGrounded; } }
		public bool  IsSneaking { get { return IsCharacterGrounded && _IsSneaking; } }
        public bool IsMoving { get { return _CharacterController.velocity.sqrMagnitude > 0 && (_Input.x != 0 || _Input.y != 0); } }
		#endregion
		
        // Use this for initialization
        private void Start()
        {
            _CharacterController = GetComponent<CharacterController>();
			_AudioSource = GetComponent<AudioSource>();
            _Controls = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<IControls>();
			_Camera = Camera.main;
            _Animator = GetComponent<Animator>();
			
            
            _OriginalCameraPosition = _Camera.transform.localPosition;
            _FovKick.Setup(_Camera);
            _HeadBob.Setup(_Camera, _StepInterval);
			_MouseLook.Init(transform , _Camera.transform);
			
            _StepCycle = 0f;
            _NextStep = _StepCycle/2f;
            _Jumping = false;
			_CharacterHeight = _CharacterController.height;
        }


        // Update is called once per frame
        private void Update()
        {
            RotateView();
            HandleJumping();
            _PreviouslyGrounded = IsCharacterGrounded;
        }

        private void HandleJumping()
        {
            if (!_Jump && IsCharacterGrounded)
                _Jump = _Controls.Jump;
			
            if (!_PreviouslyGrounded && IsCharacterGrounded)
                OnCharacterLand();
			
            if (!_CharacterController.isGrounded && !_Jumping && _PreviouslyGrounded)
                _MoveDir.y = 0f;
        }


        private void OnCharacterLand()
        {
            StartCoroutine(_JumpBob.DoBobCycle());
            PlaySound(_LandSound);
            _NextStep = _StepCycle + .5f;
            _MoveDir.y = 0f;
            _Jumping = false;
        }

        private void FixedUpdate()
        {
            float speed;
            GetInput(out speed);

            MovePlayer(speed);
            ProgressStepCycle(speed);
            UpdateCameraPosition(speed);

            if (IsSneaking)
                ChangeHeight(_CharacterHeight * _SneakHeightModifier);
            else
                ChangeHeight(_CharacterHeight);

            _Animator.SetBool("IsSneaking", _IsSneaking);
            _Animator.SetFloat("Speed", IsMoving? speed : -1);
        }

        private void ChangeHeight(float newCharacterHeight)
        {
            var initialHeight = _CharacterController.height;
            var newHeight = Mathf.Lerp(_CharacterController.height, newCharacterHeight,
                Time.fixedDeltaTime * _SneakHeightLerpSpeed);
            var heightChange = newHeight - initialHeight;

            if (heightChange > 0f)
            {
                RaycastHit hitInfo;
                Physics.SphereCast(transform.position, _CharacterController.radius, Vector3.up, out hitInfo,
                                   newHeight / 2f);
                if (hitInfo.collider != null && hitInfo.distance < newHeight)
                    return;

                transform.position = new Vector3(transform.position.x, transform.position.y + heightChange, transform.position.z);
            }
            _CharacterController.height = newHeight;
        }

        private void MovePlayer(float speed)
        {
            UpdateMoveDirection(speed);

            if (IsCharacterGrounded)
            {
                _MoveDir.y = -_StickToGroundForce;
                if (_Jump)
                    Jump();
            }
            else
                ApplyGravity();
			
            _CollisionFlags = _CharacterController.Move(_MoveDir * Time.fixedDeltaTime);
        }

        private void Jump()
        {
            _MoveDir.y = _JumpHeight;
            PlaySound(_JumpSound);
            _Jump = false;
            _Jumping = true;
        }

        private void ApplyGravity()
        {
            _MoveDir += Physics.gravity * _GravityMultiplier * Time.fixedDeltaTime;
        }

        private void UpdateMoveDirection(float speed)
        {
            Vector3 desiredMove = transform.forward * _Input.y + transform.right * _Input.x;

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, _CharacterController.radius, Vector3.down, out hitInfo,
                               _CharacterController.height / 2f);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            _MoveDir.x = desiredMove.x * speed;
            _MoveDir.z = desiredMove.z * speed;
        }


        private void PlaySound(AudioClip sound)
        {
            _AudioSource.clip = sound;
            _AudioSource.Play();
        }


        private void ProgressStepCycle(float speed)
        {
            if (IsMoving)
            {
                _StepCycle += (_CharacterController.velocity.magnitude + speed)*
                             Time.fixedDeltaTime;
            }

            if (!IsNextStep)
            {
                return;
            }

            _NextStep = _StepCycle + _StepInterval;

            PlayAudio();
        }






        private void PlayAudio()
        {
            if (!IsCharacterGrounded)
                return;
			
            if (IsSneaking)
                PlayFootStepAudio(_SneakingSounds, _SneakSoundVolumeScale);
            else
                PlayFootStepAudio(_MoveSounds);
        }

        private void PlayFootStepAudio(AudioClip[] moveSounds, float volumeScale = 1f)
        {
            if (moveSounds.Length == 0)
                return;

            int firstSoundIndex = 1;
            if (moveSounds.Length == 1)
                firstSoundIndex = 0;

            PlayRandomSound(moveSounds, volumeScale, firstSoundIndex);
        }

        private void PlayRandomSound(AudioClip[] moveSounds, float volumeScale, int firstSoundIndex)
        {
            int n = Random.Range(firstSoundIndex, moveSounds.Length);
            _AudioSource.clip = moveSounds[n];
            _AudioSource.PlayOneShot(_AudioSource.clip, volumeScale);
            moveSounds[n] = moveSounds[0];
            moveSounds[0] = _AudioSource.clip;
        }


        private void UpdateCameraPosition(float speed)
        {
            Vector3 newCameraPosition;
            if (!_UseHeadBob)
                return;
			
            if (_CharacterController.velocity.magnitude > 0 && _CharacterController.isGrounded)
            {
                _Camera.transform.localPosition =
                    _HeadBob.DoHeadBob(_CharacterController.velocity.magnitude +
                                      (speed*(_IsSneaking ? 1f : _RunstepLenghten)));
                newCameraPosition = _Camera.transform.localPosition;
                newCameraPosition.y = _Camera.transform.localPosition.y - _JumpBob.Offset();
            }
            else
            {
                newCameraPosition = _Camera.transform.localPosition;
                newCameraPosition.y = _OriginalCameraPosition.y - _JumpBob.Offset();
            }
            _Camera.transform.localPosition = newCameraPosition;
        }




        private void GetInput(out float speed)
        {
            // Read input
            float horizontal = _Controls.HorizontalMove;
            float vertical = _Controls.VerticalMove;

            bool wassneaking = _IsSneaking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            _IsSneaking = _Controls.Sneaking;
#endif
            speed = MovingSpeed;

            _Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (_Input.sqrMagnitude > 1)
            {
                _Input.Normalize();
            }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            if (_IsSneaking != wassneaking && _UseFovKick && _CharacterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();
                StartCoroutine(!_IsSneaking ? _FovKick.FOVKickUp() : _FovKick.FOVKickDown());
            }
        }


        private void RotateView()
        {
            _MouseLook.LookRotation (transform, _Camera.transform);
        }


        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }
            body.AddForceAtPosition(_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
        }
    }
}
