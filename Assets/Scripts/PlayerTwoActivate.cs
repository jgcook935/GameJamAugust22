using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoActivate : MonoBehaviour, IClickable
{
    CharacterManager characterManager;
    [SerializeField] GameObject dialogBoxPrefab;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] BoolSO hasPlayerTwo;
    GameObject dialogBox;
    bool beenClicked = false;


    void Start()
    {
        characterManager = CharacterManager.Instance;
    }

    public void Click()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            dialogBox = Instantiate(dialogBoxPrefab, transform);
            dialogBox.GetComponent<DialogBoxController>().SetText(GetComponent<ISign>().text);
            characterManager.AddPlayer(playerMovement);
            hasPlayerTwo.Value = true;
        }
    }
}