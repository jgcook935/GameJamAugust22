using System.Collections.Generic;
using UnityEngine;

public class RacoonText : MonoBehaviour, ISign
{
    public List<string> text { get; set; } = new List<string>
    {
        "They call me Swift Clif",
        "Ain't nobody faster than me",
        "Care for a race? Win and I'll give you my shiny key",
        "Ready? GO!!!"
    };
}
