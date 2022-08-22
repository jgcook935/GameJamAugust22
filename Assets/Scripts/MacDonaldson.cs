using System.Collections.Generic;
using UnityEngine;

public class MacDonaldson : MonoBehaviour, ISign
{
    public List<string> text { get; set; } = new List<string>
    {
        "This place is no good for you, kid.",
        "...",
        "Better get movin', I ain't sayin' it again"
    };
}
