using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyWaves : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public EnemyController[] enemies;
        public int enemyCounter;
        public float timeBetweenSpawning;
    }
    public Wave[] waves;

    public Transform[] pointsOfSpawning;
    public float timeBetweenWaves;
    private Wave currentWave;
    private int currentWaveIndex;
    private Transform player;
    private bool finishedSpawning;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentWaveIndex = 0;
        StartCoroutine(StartNextWave(currentWaveIndex));
    }

    IEnumerator StartNextWave(int waveIndex)
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        Debug.Log("Starting wave");
        StartCoroutine(CreatingWave(waveIndex));
    }

    IEnumerator CreatingWave(int waveIndex)
    {
        Debug.Log("Wave");
        currentWave = waves[waveIndex];
        for (int i = 0; i < currentWave.enemyCounter; i++)
        {
            if (player == null)
            {
                yield break;
            }
            EnemyController randomEnemy = currentWave.enemies
                [Random.Range(0, currentWave.enemies.Length)];
            Transform randomPointOfSpawning =
                pointsOfSpawning[Random.Range(0, pointsOfSpawning.Length)];
            Instantiate(randomEnemy,
                randomPointOfSpawning.position, randomPointOfSpawning.rotation);
            if (i == currentWave.enemyCounter - 1)
            {
                finishedSpawning = true;
            }
            else
            {
                finishedSpawning = false;
            }
            Debug.Log(i);
            yield return new WaitForSeconds(currentWave.timeBetweenSpawning);
        }
    }

    private void Update()
    {
        if (finishedSpawning &&
            GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            finishedSpawning = false;
            if (currentWaveIndex + 1 < waves.Length)
            {
                currentWaveIndex++;
                StartCoroutine(StartNextWave(currentWaveIndex));
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
