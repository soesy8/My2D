using System.Collections;
using UnityEngine;

namespace MyBird
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private GameObject pipePrefab;
        [SerializeField] private float minSpawnInterval = 0.95f;
        [SerializeField] private float maxSpawnInterval = 1.05f;
        [SerializeField] private float minHeight = -1.5f;
        [SerializeField] private float maxHeight = 3f;

        private Coroutine spawnRoutine;

        private void OnEnable()
        {
            StartSpawning();
        }

        private void OnDisable()
        {
            StopSpawning();
        }

        public void StartSpawning()
        {
            if (spawnRoutine == null)
                spawnRoutine = StartCoroutine(SpawnLoop());
        }

        public void StopSpawning()
        {
            if (spawnRoutine != null)
            {
                StopCoroutine(spawnRoutine);
                spawnRoutine = null;
            }
        }

        private IEnumerator SpawnLoop()
        {
            while (true)
            {
                if (pipePrefab == null)
                {
                    Debug.LogWarning("SpawnManager: pipePrefab is not assigned.");
                    yield break;
                }

                float wait = Random.Range(minSpawnInterval, maxSpawnInterval);
                yield return new WaitForSeconds(wait);

                Vector3 spawnPos = transform.position;
                float y = Random.Range(minHeight, maxHeight);
                spawnPos.y = y;

                Instantiate(pipePrefab, spawnPos, Quaternion.identity);
                // 한 번에 하나씩 스폰하므로 루프가 다시 대기
            }
        }
    }
}
