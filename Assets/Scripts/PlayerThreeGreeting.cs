using System.Collections.Generic;
using UnityEngine;

public class PlayerThreeGreeting : MonoBehaviour, ISign
{
    public List<string> text { get; set; } = new List<string>
    {
    "Thanks kid, I'm in your debt",
    };
}