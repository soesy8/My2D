using UnityEngine;

namespace My2DGame
{
    /// <summary>
    /// HitBox에 부착되어 Damageable 충돌체에게 데미지 주는 클래스
    /// </summary>
    public class Attack : MonoBehaviour
    {
        #region Variables
        //공격력
        [SerializeField] private float atkDamage = 10f;

        //넉백 효과
        [SerializeField] private Vector2 knockback = Vector2.zero;

        //공격 효과 처리
        public GameObject damageEffectPrefab;
        #endregion

        #region Unity Event Method
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Damageable damageable = collision.GetComponent<Damageable>();

            if (damageable != null)
            {
                Transform directionTrans =
                    transform.parent != null ?
                    transform.parent :
                    transform;

                Vector2 deliveredKnockback =
                    directionTrans.localScale.x > 0
                    ? knockback
                    : new Vector2(-knockback.x, knockback.y);

                damageable.TakeDamage(
                    atkDamage,
                    deliveredKnockback
                );
            }
        }
        #endregion
    }
}