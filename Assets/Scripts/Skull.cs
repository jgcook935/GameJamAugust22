using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : MonoBehaviour, IClickable
{
    [SerializeField] BoolSO pickedUpSkull;
    [SerializeField] BoolSO attachedSkull;
    [SerializeField] GameObject skullPrefab;

    private GameObject skull;

    void Start()
    {
        if (!pickedUpSkull.Value) skull = Instantiate(skullPrefab, transform);
    }

    public void Click()
    {
        if (attachedSkull.Value) return;
        else
        {
            pickedUpSkull.Value = true;
            Destroy(skull);
        }
    }
}
