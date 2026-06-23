using UnityEngine;
using System.Collections.Generic;

namespace My2DGame
{
    /// <summary>
    /// 충돌체 감지 클래스
    /// </summary>
    public class DetectionZone : MonoBehaviour
    {
        //감지된 콜라이더 리스트
        private List<Collider2D> detectedColliders = new List<Collider2D>();

        public bool IsDetected => detectedColliders.Count > 0;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                detectedColliders.Add(collision);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                detectedColliders.Remove(collision);
            }
        }
    }
}