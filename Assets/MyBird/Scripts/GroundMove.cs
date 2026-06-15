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
            if (transform.localPosition.x < -8.4f)
            {
                transform.localPosition = new Vector3(transform.localPosition.x + 8.4f,transform.localPosition.y);
            }
        }

    }
}