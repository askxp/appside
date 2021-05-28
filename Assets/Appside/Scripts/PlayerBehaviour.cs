using System;
using System.Collections;
using UnityEngine;

namespace Appside.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    public class PlayerBehaviour : MonoBehaviour
    {
        // private static readonly string Debug = "TestPlayerBehaviour";
        [SerializeField]
        private InputHandler inputHandler;
        [SerializeField] 
        private float acceleration;
        [SerializeField] 
        private GameObject effect;
        [SerializeField] 
        private Vector3 jumpSpeed = new Vector3(0,5f, 0);

        private bool _isGrounded = true;
        private float _currentSpeed;
        private Rigidbody _rb;
        //[SerializeField] private float speed = 0.5f;

        private Animator _animator;
        private static readonly int Speed = Animator.StringToHash("speed");
        private static readonly int Rotation = Animator.StringToHash("rotation");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody>();
        }

        void Start()
        {
            inputHandler.onJump += () =>
            {
                if(!_isGrounded) return;
                Jump();
            };
        }
    

        private void Jump()
        {
            _rb.useGravity = true;
            _animator.applyRootMotion = false;
            _animator.Play("Jumping");
            // _animator.SetBool("isJumping", true);
            _rb.velocity = jumpSpeed;
            _isGrounded = false;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (_isGrounded) return;
            if (other.gameObject.CompareTag("Ground"))
            {
                _rb.velocity = Vector3.zero;
                _rb.useGravity = false;
                _animator.applyRootMotion = true;
                // _animator.SetBool("isJumping", false);
                _animator.Play("Idle");
                _isGrounded = true;
                StartCoroutine(nameof(MakeExplosion));
            }
        }

        private IEnumerator MakeExplosion()
        {
            effect.transform.position = transform.position;
            effect.SetActive(true);
            yield return new WaitForSeconds(1f);
        }
    

        void FixedUpdate()
        {
            if(!_isGrounded) return;
            Vector2 move = inputHandler.GetMoveVector();
            float inputSpeed = move.magnitude;
            if (Math.Abs(inputSpeed) < 0.001)
            {
                _currentSpeed = 0;
            }
            _currentSpeed += ((_currentSpeed < inputSpeed)?1:-1) * acceleration * Time.deltaTime;
            Vector3 forward3 = transform.forward;
            Vector2 forward2 = new Vector2(forward3.x, forward3.z);
            float angle = Vector2.SignedAngle(move, forward2);
        
            _animator.SetFloat(Speed, _currentSpeed);
            _animator.SetFloat(Rotation, angle/180f);
        
            //HOTFIX: set rotation
            if (_currentSpeed > 0.5 && _currentSpeed < 1.5) 
                transform.forward = new Vector3(move.x, forward3.y, move.y).normalized;
        }
    }
}
