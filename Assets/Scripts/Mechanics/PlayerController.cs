using Platformer.Core;
using Platformer.Model;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;

namespace Platformer.Mechanics
{
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;

        public float timeScaleLimit = 0.1f;

        public float dashSpeed = 10f;
        public float dashDeceleration = 20f;
        private Rigidbody2D rb;
        private bool isDashing = false;
        private Vector2 lastMoveDirection = Vector2.right;

        public float maxSpeed = 7;
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        public Collider2D collider2d;
        public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;

        bool jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }

        protected override void Update()
        {
            if (controlEnabled)
            {
                move.x = Input.GetAxis("Horizontal");

                // 简化跳跃输入检测
                if (Input.GetButtonDown("Jump") && jumpState == JumpState.Grounded)
                {
                    jump = true;
                }

                if (Input.GetButton("shift"))
                    Time.timeScale = timeScaleLimit;
                else
                    Time.timeScale = 1;

                Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0).normalized;
                if (moveInput != Vector2.zero)
                    lastMoveDirection = moveInput;

                if (Input.GetButtonDown("dodge") && !isDashing)
                {
                    StartCoroutine(Dash());
                }
            }
            else
            {
                move.x = 0;
            }

            UpdateJumpState();
            base.Update();
        }

        void UpdateJumpState()
        {
            // 检测是否在地面
            if (IsGrounded)
            {
                jumpState = JumpState.Grounded;
            }
            else
            {
                if (jumpState == JumpState.Grounded)
                {
                    jumpState = JumpState.InFlight;
                }
            }
        }

        protected override void ComputeVelocity()
        {
            // 处理跳跃
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed;
                jump = false;
                jumpState = JumpState.InFlight;
                if (jumpAudio != null)
                    audioSource.PlayOneShot(jumpAudio);
            }

            // 处理角色朝向
            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            // 更新动画参数
            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        private IEnumerator Dash()
        {
            isDashing = true;
            float originalGravity = rb.gravityScale;
            rb.gravityScale = 0;

            float dashDirectionX = lastMoveDirection.x != 0 ?
                Mathf.Sign(lastMoveDirection.x) :
                (spriteRenderer.flipX ? -1 : 1);
            Vector2 dashDirection = new Vector2(dashDirectionX, 0);

            float currentSpeed = dashSpeed;

            while (currentSpeed > 0)
            {
                float rayDistance = collider2d.bounds.extents.x + 0.1f;
                RaycastHit2D hit = Physics2D.Raycast(
                    transform.position,
                    dashDirection,
                    rayDistance,
                    LayerMask.GetMask("Ground")
                );

                if (hit.collider != null)
                {
                    break;
                }

                rb.velocity = dashDirection * currentSpeed;
                currentSpeed -= dashDeceleration * Time.deltaTime;

                yield return null;
            }

            rb.velocity = Vector2.zero;
            rb.gravityScale = originalGravity;
            isDashing = false;
        }
     
            public enum JumpState
        {
            Grounded,
            InFlight
        }
    }
}