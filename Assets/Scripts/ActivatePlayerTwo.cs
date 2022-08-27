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
    bool beenClicked = false;

    public void Click()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            dialogBox = Instantiate(dialogBoxPrefab, transform);
            dialogBox.GetComponent<DialogBoxController>().SetText(GetComponent<ISign>().text);
            characterManager.AddPlayer(playerMovement);
        }
    }
}
