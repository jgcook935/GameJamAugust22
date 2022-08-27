using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject slimePrefab;
    public GameObject[] spawnPoints;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed, spawning enemy");
            SpawnEnemyRandom();
        }
    }

    void SpawnEnemyRandom()
    {
        var point = Random.Range(0, 3);
        Instantiate(slimePrefab, spawnPoints[point].transform);
    }
}
