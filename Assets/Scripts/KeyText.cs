using System.Collections.Generic;
using UnityEngine;

public class KeyText : MonoBehaviour, ISign
{
    public List<string> text { get; set; } = new List<string>
    {
        "Hey! Get your dirty paws off my key!",
    };
}