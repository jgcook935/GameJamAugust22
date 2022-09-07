using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private bool raceOver = false;
    [SerializeField] SwiftClif swiftClif;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!raceOver)
        {
            if (!swiftClif.raceStarted || collider.gameObject.tag != "Player") return;
            swiftClif.FinishRace(collider);
            raceOver = true;
        }
    }
}
