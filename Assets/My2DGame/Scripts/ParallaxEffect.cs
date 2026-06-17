using UnityEngine;

namespace My2DGame
{
    /// <summary>
    /// 배경의 패럴랙스 효과를 구현하는 클래스
    /// </summary>
    public class ParallaxEffect : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Transform playerTransform;

        private Vector3 startPosition;

        private float parallaxScale;

        private void Start()
        {
            startPosition = transform.position;

            float cameraDistance =
                playerTransform.position.z -
                cameraTransform.position.z;

            float backgroundDistance =
                transform.position.z -
                cameraTransform.position.z;

            parallaxScale =
                cameraDistance / backgroundDistance;

            // 효과 강화
            parallaxScale *= parallaxScale;
        }

        private void LateUpdate()
        {
            float cameraX = cameraTransform.position.x;

            transform.position = new Vector3(
                startPosition.x +
                cameraX * (1f - parallaxScale),
                startPosition.y,
                startPosition.z
            );
        }
    }
}
