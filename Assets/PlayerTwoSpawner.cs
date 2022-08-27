using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoSpawner : MonoBehaviour
{
    [SerializeField]
    BoolSO hasPlayerTwoSO;

    [SerializeField]
    GameObject playerTwoPrefab;


    void Start()
    {
        Debug.Log(hasPlayerTwoSO.Value);
        if (!hasPlayerTwoSO.Value)
        {
            Instantiate(playerTwoPrefab, transform);
            Debug.Log("spawned prefab");
        }
    }
}
