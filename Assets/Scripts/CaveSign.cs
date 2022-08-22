using System.Collections.Generic;
using UnityEngine;

public class CaveSign : MonoBehaviour, ISign
{
    public List<string> text { get; set; } = new List<string>
    {
        "6 million ways to die",
        "Choose one",
    };
}
