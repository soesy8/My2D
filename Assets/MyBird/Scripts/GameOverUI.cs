using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyBird
{
    /// <summary>
    /// 
    /// </summary>
    public class GameOverUI : MonoBehaviour
    {
        //다시하기 버튼
        public void Retry()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.Restart();
            }
        }

    }
}