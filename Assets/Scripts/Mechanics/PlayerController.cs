using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;

        public float timeScaleLimit = 0.1f;

        public float dashSpeed = 10f;         // 冲刺初始速度
        public float dashDeceleration = 20f;  // 冲刺减速度（单位：速度/秒）
        private Rigidbody2D rb;
        private bool isDashing = false;
        private Vector2 lastMoveDirection = Vector2.right; // 默认向右

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        /*internal new*/ public Collider2D collider2d;
        /*internal new*/ public AudioSource audioSource;
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
            Schedule<LogMessageTest>();
            rb = GetComponent<Rigidbody2D>(); 
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; // 防止高速穿墙
        }

        protected override void Update()
        {
            if (controlEnabled)
            {
                move.x = Input.GetAxis("Horizontal");
                if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                    jumpState = JumpState.PrepareToJump;
                else if (Input.GetButtonUp("Jump"))
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
                

                if (Input.GetButton("shift")) Time.timeScale = timeScaleLimit;
                else Time.timeScale = 1;


                // 获取玩家输入方向
                Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
                if (moveInput != Vector2.zero)
                {
                    lastMoveDirection = moveInput;
                }
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
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
            }

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        private IEnumerator Dash()
        {
            isDashing = true;
            float originalGravity = rb.gravityScale;
            rb.gravityScale = 0;

            // 计算冲刺方向（仅水平）
            float dashDirectionX = lastMoveDirection.x != 0 ?
                Mathf.Sign(lastMoveDirection.x) :
                (spriteRenderer.flipX ? -1 : 1);
            Vector2 dashDirection = new Vector2(dashDirectionX, 0);

            // 初始速度
            float currentSpeed = dashSpeed;

            // 冲刺过程（速度递减）
            while (currentSpeed > 0)
            {
                // 撞墙检测
                float rayDistance = collider2d.bounds.extents.x + 0.1f;
                RaycastHit2D hit = Physics2D.Raycast(
                    transform.position,
                    dashDirection,
                    rayDistance,
                    LayerMask.GetMask("Ground")
                );

                if (hit.collider != null)
                {
                    Debug.Log("Hit wall, stopping dash");
                    break;
                }

                // 应用当前速度
                rb.velocity = dashDirection * currentSpeed;
                currentSpeed -= dashDeceleration * Time.deltaTime;

                yield return null;
            }

            // 重置状态
            rb.velocity = Vector2.zero;
            rb.gravityScale = originalGravity;
            isDashing = false;
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }
    }
}