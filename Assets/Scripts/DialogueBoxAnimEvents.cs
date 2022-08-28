using System;
using UnityEngine;

public class DialogueBoxAnimEvents : MonoBehaviour
{
    public Action OnDestroyingDialogueBox;

    public void OnAnimEnd()
    {
        OnDestroyingDialogueBox?.Invoke();
        Destroy(transform.parent.parent.gameObject);
    }
}
