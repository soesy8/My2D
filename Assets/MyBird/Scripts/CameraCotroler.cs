using UnityEngine;

namespace MyBird
{
    public class CameraCotroler : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f);
        [SerializeField] private float smoothSpeed = 5f;
        [SerializeField] private bool followX = true;
        [SerializeField] private bool followY = true;

        void Awake()
        {
            if (target == null)
            {
                // Player 컴포넌트를 가진 오브젝트를 찾아 자동 할당
                var player = FindObjectOfType<Player>();
                if (player != null)
                    target = player.transform;
            }
        }

        void LateUpdate()
        {
            if (target == null) return;

            Vector3 desired = transform.position;
            if (followX) desired.x = target.position.x + offset.x;
            if (followY) desired.y = target.position.y + offset.y;
            // 항상 카메라의 z 축은 offset.z로 설정
            desired.z = target.position.z + offset.z;

            transform.position = desired;
            //transform.position = Vector3.Lerp(transform.position, desired, smoothSpeed * Time.deltaTime);
        }
    }
}
