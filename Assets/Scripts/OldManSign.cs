using System.Collections.Generic;
using UnityEngine;

public class OldManSign : MonoBehaviour, ISign
{
    public List<string> text { get; set; } = new List<string>
    {
        "Bugger off",
        "...",
        "I mean it! Scram!"
    };
}
