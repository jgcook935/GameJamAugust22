using System.Collections.Generic;
using UnityEngine;

public class CaveSign : MonoBehaviour, ISign
{
    public List<string> text { get; set; } = new List<string>
    {
        "Entrance to a mysterious cave. Don't worry it's not scary or anything.",
        "Enter at your own risk...",
        "Oh shoot, that sounded scary.",
        "Just go in... Or don't. I don't get paid enough for this."
    };
}
