using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    int[] dragonCounts = new int[] { 3, 5, 7 };

    bool isWaveActive = false;
    bool hasInitiated = false;
    public bool hasFinished = false;

    [SerializeField] GameObject dialogBoxPrefab;
    GameObject dialogBox;

    public List<string> finalText { get; set; } = new List<string>
    {
        "With the final enemies vanquished...",
        "Our hero has saved the day...",
        "And made some friends along the way",
    };

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
                Finish();
            }
        }
    }

    void OnTriggerEnter2D()
    {
        if (hasInitiated) return;
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
            Instantiate(slimePrefab, spawnPoints[Random.Range(0, 3)].transform);
            Instantiate(dragonPrefab, spawnPoints[Random.Range(4, 11)].transform);
        }
        slimesAlive = slimeCounts[currentWave];
        dragonsAlive = dragonCounts[currentWave];
        Debug.Log($"we called start wave {currentWave}, {slimesAlive}");
    }

    void Finish()
    {
        if (dialogBox != null) return;
        dialogBox = Instantiate(dialogBoxPrefab, transform);
        dialogBox.GetComponent<DialogBoxController>().SetText(finalText);
        dialogBox.GetComponentInChildren<DialogueBoxAnimEvents>().OnDestroyingDialogueBox += Quit;
    }

    void Quit()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
