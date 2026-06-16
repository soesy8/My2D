using UnityEngine;

namespace MyBird
{
    public class GroundMove : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 2f;

        private void Update()
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
            float width = 8.4f;
            if (transform.localPosition.x < -width)
            {
                // 오른쪽으로 한 칸 옮겨 루핑
                transform.localPosition = new Vector3(transform.localPosition.x + width * 2f, transform.localPosition.y, transform.localPosition.z);
            }
        }

    }
}