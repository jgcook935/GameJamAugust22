using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePlayerTwo : MonoBehaviour, IClickable
{
    [SerializeField] CharacterManager characterManager;
    [SerializeField] Sign greetingSign;
    [SerializeField] GameObject dialogBoxPrefab;
    [SerializeField] PlayerMovement playerMovement;
    GameObject dialogBox;

    public void Click()
    {
        if (!characterManager.followPlayer2)
        {
            dialogBox = Instantiate(dialogBoxPrefab, transform);
            dialogBox.GetComponent<DialogBoxController>().SetText(GetComponent<ISign>().text);
            characterManager.AddPlayer(playerMovement);
        }
    }
}
