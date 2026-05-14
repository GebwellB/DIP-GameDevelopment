using System.Collections;
using UnityEngine;

namespace GOAP
{
    public class PTSDChickenSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject PTSDChickenPrefab;

        [SerializeField]
        private Transform spawnPoint;

        [SerializeField]
        private int amountToSpawn = 1;

        private bool firstRun = true;

        public void Spawn()
        {
            Instantiate(PTSDChickenPrefab, spawnPoint.position, spawnPoint.rotation);
        }

        private void Update()
        {
            if (NPCGOAPHandler.gameRunning && NPCGOAPHandler.readyToSpawn && firstRun)
            {
                SpawnStartingChickens();
                firstRun = false;
            }

            if (NPCGOAPHandler.gameRunning && NPCGOAPHandler.readyToSpawn && Input.GetKeyDown(KeyCode.F))
            {
                SpawnSingleChicken();
            }
        }

        public void SpawnStartingChickens()
        {
            StartCoroutine(SpawnRoutine());
        }

        public void SpawnSingleChicken()
        {
            Instantiate(PTSDChickenPrefab, spawnPoint.position, spawnPoint.rotation);
        }

        private IEnumerator SpawnRoutine()
        {
            for (int i = 0; i < amountToSpawn; i++)
            {
                Instantiate(PTSDChickenPrefab, spawnPoint.position, spawnPoint.rotation);
                yield return new WaitForSeconds(1.5f);
            }
        }
    }
}
