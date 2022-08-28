using UnityEngine;

public class SlimeWave : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject slimePrefab;
    public GameObject foodPrefab;

    [SerializeField] BoolSO hasAttemptedRace;
    [SerializeField] BoolSO hasPlayerTwo;

    BoxCollider2D boxCollider;

    int waveCount = 2;
    int currentWave = -1;
    int slimesAlive = 0;
    int[] slimeCounts = new int[] { 1, 3, 5 };

    bool isWaveActive = false;
    bool hasInitiated = false;
    public bool hasFinished = false;

    static SlimeWave _instance;
    public static SlimeWave Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<SlimeWave>();
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
        if (slimesAlive < slimeCounts[currentWave])
        {
            isWaveActive = false;
        }
    }

    void Update()
    {
        if (!hasInitiated) return;

        if (!isWaveActive && slimesAlive == 0)
        {
            if (currentWave != waveCount)
            {
                StartWave();
            }
            else
            {
                hasInitiated = false;
                hasFinished = true;
                Instantiate(foodPrefab, spawnPoints[0].transform);
                PlayerTwoSpawner.Instance.SpawnPlayerTwo();
            }
        }
    }

    void OnTriggerEnter2D()
    {
        if (hasAttemptedRace.Value == false || hasPlayerTwo.Value == true)
        {
            Debug.Log("we've already attempted the race");
            return;
        }
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
