using UnityEngine;

namespace MyBird
{
    public class Player : MonoBehaviour
    {
        private enum State
        {
            Waiting,
            Playing,
            Dead
        }

        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float tiltSmooth = 5f;

        // 올라갈 때 최대 각도 (+30도), 내려갈 때 최소 각도 (-90도)
        [SerializeField] private float maxTilt = 30f;
        [SerializeField] private float minTilt = -90f;
        [SerializeField] private float angleMultiplier = 6f;
        // 오른쪽으로 일정 속도로 이동 (Rigidbody2D.velocity 사용)
        [SerializeField] private float moveSpeed = 2f;
        // 대기 상태에서 아래로 떨어질 때 보정해주는 힘
        [SerializeField] private float hoverForce = 20f;

        private State state = State.Waiting;
        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            switch (state)
            {
                case State.Waiting:
                    // 제자리에서 x 고정
                    rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
                    // 아래로 떨어질 때만 위로 힘을 줘서 대기(hover) 유지
                    if (rb.linearVelocity.y < 0f)
                    {
                        rb.AddForce(Vector2.up * hoverForce, ForceMode2D.Force);
                    }
                    break;

                case State.Playing:
                    // 오른쪽으로 일정 속도로 이동
                    rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
                    break;

                case State.Dead:
                    rb.linearVelocity = Vector2.zero;
                    break;
            }
        }

        private void Update()
        {
            bool inputDown = Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);

            if (inputDown)
            {
                if (state == State.Waiting)
                {
                    // 대기 -> 플레이로 전환, 첫 입력은 점프로 처리
                    state = State.Playing;
                    Jump();
                }
                else if (state == State.Playing)
                {
                    Jump();
                }
            }

            UpdateRotation();
        }

        private void Jump()
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        private void UpdateRotation()
        {
            float angle = Mathf.Clamp(rb.linearVelocity.y * angleMultiplier, minTilt, maxTilt);
            Quaternion target = Quaternion.Euler(0f, 0f, angle);
            transform.rotation = Quaternion.Lerp(transform.rotation, target, tiltSmooth * Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // 파이프 등과의 충돌로 게임오버 처리
            state = State.Dead;
            // GameManager에 게임오버 알림
            if (GameManager.Instance != null)
                GameManager.Instance.GameOver();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Point"))
            {
                if (GameManager.Instance != null)
                    GameManager.Instance.AddScore(1);
            }
        }

        public void Revive()
        {
            state = State.Waiting;
            rb.linearVelocity = Vector2.zero;
            transform.rotation = Quaternion.identity;
        }

        public void Die()
        {
            state = State.Dead;
            rb.linearVelocity = Vector2.zero;
        }
    }
}
