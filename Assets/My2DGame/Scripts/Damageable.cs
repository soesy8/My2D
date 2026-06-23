using UnityEngine;
using UnityEngine.Events;

namespace My2DGame
{
    /// <summary>
    /// 캐릭터의 체력을 관리하는 클래스
    /// </summary>
    public class Damageable : MonoBehaviour
    {
        #region Variables
        private Animator animator;
        [SerializeField] private float maxHp = 100f;
        private float curHp;

        private bool isInvincible = false;
        [SerializeField]
        private float invincibleTimer = 0.5f;
        private float countdown = 0f;

        private bool isDeath = false;

        //데메지 입을 때 등록된 이벤트를 호출하는 이벤트 함수 정의
        public UnityAction<float, Vector2> hitAction;
        #endregion


        #region Property
        public float MaxHp
        {
            get { return maxHp; }
            private set { maxHp = value; }
        }

        public float CurHp
        {
            get { return curHp; }
            private set
            {  
                curHp = value; 
            }
        }

        public bool IsDeath
        {
            get { return isDeath; }
            private set 
            {  
                isDeath = value;
                animator.SetBool(AnimationString.isDeath, value);
            }
        }
        #endregion


        #region Unity Event Method
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            CurHp = MaxHp;
        }

        private void Update()
        {
            if (isDeath) return;

            if (isInvincible)
            {
                countdown += Time.deltaTime;
                if (countdown > invincibleTimer)
                {
                    isInvincible = false;
                    countdown = 0f;
                }
            }
        }
        #endregion


        #region Custom Method
        public void TakeDamage(float damage, Vector2 knockback)
        {
            if (isDeath || isInvincible) return;

            CurHp -= damage;
            isInvincible = true;
            animator.SetTrigger(AnimationString.hitTrigger);
            //animator.SetBool(AnimationString.cannotMove, true);

            if (hitAction != null)
            {
                hitAction.Invoke(damage, knockback);
            }


            if (CurHp <= 0)
            {
                Die();
            }
        }

        void Die()
        {
            IsDeath = true;


        }
        #endregion
    }
}