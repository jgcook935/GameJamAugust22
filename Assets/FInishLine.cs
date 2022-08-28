using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FInishLine : MonoBehaviour
{
    private bool raceOver = false;
    [SerializeField] SwiftClif swiftClif;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!raceOver)
        {
            if (collision.gameObject.tag != "Player") return;
            swiftClif.FinishRace(collision);
            raceOver = true;
        }
    }
}
