using UnityEngine;

public class SlimeWaveFinal : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject slimePrefab;
    public GameObject dragonPrefab;
    public GameObject foodPrefab;

    BoxCollider2D boxCollider;

    int waveCount = 2;
    int currentWave = -1;
    int slimesAlive = 0;
    int dragonsAlive = 0;
    int[] slimeCounts = new int[] { 3, 5, 7 };
    int[] dragonCounts= new int[] { 3, 5, 7 };

    bool isWaveActive = false;
    bool hasInitiated = false;
    public bool hasFinished = false;

    static SlimeWaveFinal _instance;
    public static SlimeWaveFinal Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<SlimeWaveFinal>();
            return _instance;
        }
    }

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void DecrementSlimeCount()
    {
        slimesAlive--;
        if (slimesAlive <= slimeCounts[currentWave] && dragonsAlive <= dragonCounts[currentWave])
        {
            isWaveActive = false;
        }
    }

    public void DecrementDragonCount()
    {
        dragonsAlive--;
        if (slimesAlive <= slimeCounts[currentWave] && dragonsAlive <= dragonCounts[currentWave])
        {
            isWaveActive = false;
        }
    }

    void Update()
    {
        if (!hasInitiated) return;

        if (!isWaveActive && slimesAlive == 0 && dragonsAlive == 0)
        {
            if (currentWave != waveCount)
            {
                StartWave();
            }
            else
            {
                hasInitiated = false;
                hasFinished = true;
            }
        }
    }

    void OnTriggerEnter2D()
    {
        StartWave();
        boxCollider.enabled = false;
        hasInitiated = true;
    }

    void StartWave()
    {
        currentWave++;
        isWaveActive = true;
        for (int i = 0; i < slimeCounts[currentWave]; i++)
        {
            Instantiate(slimePrefab, spawnPoints[0].transform);
        }
        slimesAlive = slimeCounts[currentWave];
        Debug.Log($"we called start wave {currentWave}, {slimesAlive}");
    }
}
