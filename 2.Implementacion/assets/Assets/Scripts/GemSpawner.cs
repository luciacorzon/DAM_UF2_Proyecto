using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    public GameObject[] gemPrefabs; 
    public int gemCount = 10; 
    private BoxCollider2D area; 

    void Start()
    {
        area = GetComponent<BoxCollider2D>(); 

        if (area == null)
        {
            return;
        }

        SpawnGems();
    }

    void SpawnGems()
    {
        for (int i = 0; i < gemCount; i++)
        {
            float xPosition = Random.Range(area.bounds.min.x, area.bounds.max.x);
            float yPosition = Random.Range(area.bounds.min.y, area.bounds.max.y);
            Vector2 spawnPosition = new Vector2(xPosition, yPosition);

            GameObject gemPrefab = gemPrefabs[Random.Range(0, gemPrefabs.Length)];

            Instantiate(gemPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
