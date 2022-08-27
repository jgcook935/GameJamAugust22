using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoGreeting : MonoBehaviour, ISign
{
    public List<string> text { get; set; } = new List<string>
    {
        "You're not alone",
        "kid"
    };
}
