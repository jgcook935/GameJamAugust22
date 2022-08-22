using UnityEngine;

public class DialogueBoxAnimEvents : MonoBehaviour
{
    public void OnAnimEnd()
    {
        Destroy(transform.parent.parent.gameObject);
    }
}
