using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGhoul : MonoBehaviour
{
    [SerializeField] BoolSO destroyedGhoul;

    void Start()
    {
        if (destroyedGhoul.Value) Destroy(this.gameObject);
    }

    public void DoDamage(Vector2 direction)
    {
        destroyedGhoul.Value = true;
        Destroy(this.gameObject);
    }
}
