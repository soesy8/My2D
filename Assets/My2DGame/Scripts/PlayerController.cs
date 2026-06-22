using UnityEngine;
using UnityEngine.InputSystem;

namespace My2DGame
{
    /// <summary>
    /// 플레이어 캐릭터의 움직임과 행동을 제어하는 클래스
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        [Header("Move")]
        [SerializeField] private float moveSpeed = 4f;

        [Header("Run")]
        [SerializeField] private float runSpeed = 8f;

        private Rigidbody2D rb;
        private Animator animator;

        // 입력값 저장
        private float moveInput;

        //걷기 뛰기 체크
        private bool isMove = false;
        private bool isRun = false;

        //공격 중 체크 - 자체추가
        private bool isAttacking = false;

        //반전
        private bool isFacingRight = true;

        //점프 - y축의 속도를 jumpforce 값으로설정
        [SerializeField] private float jumpForce = 10f;

        private TouchingDirection touchingDirecion;
        #endregion

        public bool IsMove
        {
            get { return isMove; }
            private set 
            { 
                isMove = value;
                animator.SetBool(AnimationString.isMove, value);
            }
        }

        public bool IsRun
        {
            get { return isRun; }
            private set
            {
                isRun = value;
                animator.SetBool(AnimationString.isRun, value);
            }
        }

        public bool IsFacingRight
        {
            get { return isFacingRight; }
            private set
            { 
                if (isFacingRight != value)
                {
                    transform.localScale *= new Vector2(-1, 1);
                }
                isFacingRight = value; 
            }
        }


        #region Unity Methods

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            touchingDirecion = GetComponent<TouchingDirection>();
        }

        private void Start()
        {
            
        }


        private void FixedUpdate()
        {
            Move();
            //애니메이션 셋팅
            animator.SetFloat(AnimationString.yVelocity, rb.linearVelocity.y);
        }

        #endregion

        #region Input System

        /// <summary>
        /// New Input System의 Move Action 이벤트
        /// </summary>
        /// <param name="context">입력 정보</param>
        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            moveInput = input.x;

            IsMove = (input != Vector2.zero);

            SetFacingDirection(input);
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            IsRun = context.ReadValueAsButton();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (animator.GetBool(AnimationString.cannotMove))
                return;

            if (context.started && touchingDirecion.IsGround)
            {
                Jump();
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (animator.GetBool(AnimationString.cannotMove))
                return;

            if (context.started)
            {
                Attack();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 좌우 이동
        /// </summary>
        private void Move()
        {
            if (rb == null) return;

            if (animator.GetBool(AnimationString.cannotMove))
            {
                rb.linearVelocity = new Vector2(
                    0f,
                    rb.linearVelocity.y
                );
                return;
            }

            float speed = IsRun ? runSpeed : moveSpeed;

            rb.linearVelocity = new Vector2(
                moveInput * speed,
                rb.linearVelocity.y
            );
        }

        //방향반전
        void SetFacingDirection(Vector2 moveinput)
        {
            if (moveinput.x > 0f)
            {
                IsFacingRight = true;
            }
            else if(moveinput.x < 0f)
            {
                IsFacingRight = false;
            }
        }

        void Jump()
        {
            if (!touchingDirecion.IsGround) return;

            animator.SetTrigger(AnimationString.jumpTrigger);

            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
        }

        void Attack()
        {
            if (isAttacking) return;
            isAttacking = true;
            animator.SetTrigger(AnimationString.attackTrigger);
        }

        void EndAttack()
        {
            isAttacking = false;
        }

        #endregion
    }
}