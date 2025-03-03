using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; 
    public float initialSpawnRate = 1f; 
    public float spawnRateDecrease = 0.1f; 
    public float minSpawnRate = 0.5f;
    public float spawnDistance = 3f; 

    private float currentSpawnRate;

    void Start()
    {
        currentSpawnRate = initialSpawnRate;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(currentSpawnRate);
            SpawnEnemy();

            // O tempo de spawn vaise reducindo ata chegar ao mínimo
            currentSpawnRate = Mathf.Max(minSpawnRate, currentSpawnRate - spawnRateDecrease);
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0) return; 

        // Obtener a posición da cámara e o seu tamaño visible para spawnear enemigos arredor dela
        // de forma que aparezan cerca do xogador
        Vector3 cameraPos = Camera.main.transform.position;
        float camHeight = Camera.main.orthographicSize;
        float camWidth = camHeight * Camera.main.aspect;

        // Escóllese un lado aleatorio
        int side = Random.Range(0, 4);
        float xPosition = 0, yPosition = 0;

        switch (side)
        {
            case 0: 
                xPosition = cameraPos.x - camWidth - spawnDistance;
                yPosition = Random.Range(cameraPos.y - camHeight, cameraPos.y + camHeight);
                break;
            case 1: 
                xPosition = cameraPos.x + camWidth + spawnDistance;
                yPosition = Random.Range(cameraPos.y - camHeight, cameraPos.y + camHeight);
                break;
            case 2: 
                xPosition = Random.Range(cameraPos.x - camWidth, cameraPos.x + camWidth);
                yPosition = cameraPos.y + camHeight + spawnDistance;
                break;
            case 3: 
                xPosition = Random.Range(cameraPos.x - camWidth, cameraPos.x + camWidth);
                yPosition = cameraPos.y - camHeight - spawnDistance;
                break;
        }

        // Seleccionar un enemigo aleatorio do array, crealo e movelo hacia a cámara
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];


        Vector2 spawnPosition = new Vector2(xPosition, yPosition);
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

      
        Vector2 direction = (cameraPos - new Vector3(xPosition, yPosition)).normalized;
    }
}
