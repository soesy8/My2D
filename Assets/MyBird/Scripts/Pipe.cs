using UnityEngine;

namespace MyBird
{
    public class Pipe : MonoBehaviour
    {
        private float leftLimitX;
        private Camera mainCam;

        private void Awake()
        {
            mainCam = Camera.main;
            if (mainCam != null)
            {
                Vector3 leftBottom = mainCam.ScreenToWorldPoint(new Vector3(0, 0, Mathf.Abs(mainCam.transform.position.z)));
                leftLimitX = leftBottom.x - 1f;
            }
            else
            {
                leftLimitX = -20f;
            }
        }

        private void Update()
        {
            if (transform.position.x < leftLimitX)
            {
                Destroy(gameObject);
            }
        }
    }
}
