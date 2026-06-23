using UnityEngine;

namespace My2DGame
{
    /// <summary>
    /// 적을 관리하는 클래스
    /// </summary>
    public class EnemyController : MonoBehaviour
    {
        #region Variables
        private Rigidbody2D rb;
        private TouchingDirection touchingdirection;
        private Animator animator;
        private Damageable damageable;

        public DetectionZone detectionZone;

        [SerializeField] private float runSpeed = 5.0f;

        public enum WalkableDirection
        {
            Left,
            Right
        }

        private WalkableDirection walkDirection = WalkableDirection.Right;

        private Vector2 directionVector = Vector2.right;

        private bool hasTarget;
        private bool cannotMove;


        #endregion

        #region Property
        public WalkableDirection WalkDirecion
        {
            get { return walkDirection; }
            private set
            {
                //방향전환이 일어난 시점
                if (walkDirection != value)
                {
                    //이미지 플립
                    transform.localScale *= new Vector2(-1, 1);

                    if (value == WalkableDirection.Left)
                    {
                        directionVector = Vector2.left;
                    }
                    else if (value == WalkableDirection.Right)
                    {
                        directionVector = Vector2.right;
                    }
                }
                walkDirection = value;
            }
        }

        public bool CannotMove
        {
            get
            {
                return cannotMove;
            }
        }

        public bool HasTarget
        {
            get { return hasTarget; }
            private set
            {
                hasTarget = value;
                animator.SetBool(AnimationString.hasTarget, value);
            }
        }

        //어택 쿨타임
        public float CooldownTime
        {
            get { return animator.GetFloat(AnimationString.cooldownTime); }
            private set { animator.SetFloat(AnimationString.cooldownTime, value);}
        }

        public bool LockVelocity
        {
            get { return animator.GetBool(AnimationString.lockVelocity); }
        }
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            touchingdirection = GetComponent<TouchingDirection>();
            animator = GetComponent<Animator>();

            damageable = GetComponent<Damageable>();
            damageable.hitAction += OnHit;
        }

        private void Update()
        {
            //타겟 디텍팅
            HasTarget = detectionZone.IsDetected;

            if (CooldownTime > 0f)
            {
                CooldownTime -= Time.deltaTime;
            }
        }

        private void FixedUpdate()
        {
            if (damageable.IsDeath)
            {
                rb.linearVelocity = Vector2.zero;
                return;
            }

            //벽체크
            if (touchingdirection.IsGround && touchingdirection.IsWall)
            {
                Flip();
            }
            //이동 - 넉백 효과 동안에는 움직일 수 없다
            if (LockVelocity == true) return;
            if (cannotMove == false)
            {
                rb.linearVelocity = new Vector2(directionVector.x * runSpeed, rb.linearVelocity.y);
            }
            else
            {
                rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            }
        }

        #endregion


        void Flip()
        {
            if (walkDirection == WalkableDirection.Left)
            {
                WalkDirecion = WalkableDirection.Right;
            }
            else
            {
                WalkDirecion = WalkableDirection.Left;
            }
        }

        //데미지 이벤트 함수에 등록되는 함수
        void OnHit(float damage, Vector2 knockback)
        {
            rb.linearVelocity = new Vector2(knockback.x, rb.linearVelocity.y + knockback.y);
        }

    }
}