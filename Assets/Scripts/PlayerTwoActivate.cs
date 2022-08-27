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
        if (hasPlayerTwo.Value) beenClicked = true;
    }

    public void Click()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            dialogBox = Instantiate(dialogBoxPrefab, transform);
            dialogBox.GetComponent<DialogBoxController>().SetText(GetComponent<ISign>().text);
            characterManager.AddPlayer(playerMovement);
            gameObject.layer = 8; // change player 2 to use the player layer for physics and stuff
            hasPlayerTwo.Value = true;
        }
    }
}
