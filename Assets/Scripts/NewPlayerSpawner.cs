using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerSpawner : MonoBehaviour
{
    [SerializeField]
    BoolSO hasPlayerSO;

    [SerializeField]
    GameObject playerPrefab;


    void Start()
    {
        if (!hasPlayerSO.Value)
        {
            Instantiate(playerPrefab, transform);
        }
    }
}
