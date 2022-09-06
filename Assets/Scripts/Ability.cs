using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    private bool _enabled = true;
    public bool Enabled
    {
        get { return _enabled; }
        set { _enabled = value; }
    }

}
