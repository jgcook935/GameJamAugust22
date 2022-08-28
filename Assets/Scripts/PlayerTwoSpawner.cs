using UnityEngine;

public class PlayerTwoSpawner : MonoBehaviour
{
    public GameObject playerTwoPrefab;

    static PlayerTwoSpawner _instance;
    public static PlayerTwoSpawner Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<PlayerTwoSpawner>();
            return _instance;
        }
    }

    public void SpawnPlayerTwo()
    {
        Instantiate(playerTwoPrefab, transform);
    }
}
