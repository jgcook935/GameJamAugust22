using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoGreeting : MonoBehaviour, ISign
{
    public List<string> text { get; set; } = new List<string>
    {
        "...",
        "Woah kid, you took out all those slimes all alone",
        "I may not have sword skills like you",
        "But boy am I fast",
        "I like you pal, you've got moxy",
        "Mind if I join you?",
        "If you ever need my help, just press tab, and I'm there"
    };
}
