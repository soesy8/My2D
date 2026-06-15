using UnityEngine;

namespace MyBird
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private SpawnManager spawnManager;
        private int score = 0;
        private bool isGameOver = false;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            if (spawnManager == null)
                spawnManager = FindObjectOfType<SpawnManager>();
        }

        public void AddScore(int amount)
        {
            if (isGameOver) return;
            score += amount;
            Debug.Log("Score: " + score);
            // TODO: UI 업데이트
        }

        public void GameOver()
        {
            if (isGameOver) return;
            isGameOver = true;
            Debug.Log("Game Over");
            // 스폰 중지
            if (spawnManager != null)
                spawnManager.StopSpawning();
            // 플레이어 이동 중지: Find player and call Die()
            var player = FindObjectOfType<Player>();
            if (player != null)
            {
                player.Die();
            }
            // TODO: 게임오버 UI 표시
        }
    }
}
