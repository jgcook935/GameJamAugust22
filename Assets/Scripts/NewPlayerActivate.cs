using UnityEngine;

public class NewPlayerActivate : MonoBehaviour, IClickable
{
    CharacterManager characterManager;
    [SerializeField] GameObject dialogBoxPrefab;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] BoolSO hasPlayerSO;
    GameObject dialogBox;
    bool beenClicked = false;

    void Start()
    {
        characterManager = CharacterManager.Instance;
        if (hasPlayerSO.Value) beenClicked = true;
    }

    public void Click()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            dialogBox = Instantiate(dialogBoxPrefab, transform);
            dialogBox.GetComponent<DialogBoxController>().SetText(GetComponent<ISign>().text);
            characterManager.AddPlayer(playerMovement);
            hasPlayerSO.Value = true;
        }
    }
}
