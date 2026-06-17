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
        [SerializeField] private float moveSpeed = 5f;

        private Rigidbody2D rb;

        // 입력값 저장
        private float moveInput;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Move();
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
        }

        #endregion

        #region Methods

        /// <summary>
        /// 좌우 이동
        /// </summary>
        private void Move()
        {
            if (rb == null) return;

            rb.linearVelocity = new Vector2(
                moveInput * moveSpeed,
                rb.linearVelocity.y
            );
        }

        #endregion
    }
}