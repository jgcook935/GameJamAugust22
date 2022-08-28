using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour, IClickable
{
    [SerializeField] BoolSO pickedUpSkull;
    [SerializeField] BoolSO attachedSkull;
    [SerializeField] BoolSO hasSword;
    [SerializeField] GameObject skullPrefab;
    [SerializeField] GameObject dialogBoxPrefab;
    [SerializeField] GameObject swordGraphicsPrefab;
    GameObject dialogBox;
    private bool turnedSwordOff = false;
    public List<string> text { get; set; } = new List<string>
    {
        "...",
    };

    public List<string> thanksText { get; set; } = new List<string>
    {
        "Hey thanks, kid",
        "Take this sword",
    };

    public List<string> finalText { get; set; } = new List<string>
    {
        "Huh? Oh you.",
        "Go slice those boxes up to clear the path",
    };

    void Start()
    {
        if (!attachedSkull) Instantiate(skullPrefab, transform);
    }

    void Update()
    {
        if (!turnedSwordOff && !hasSword.Value && !attachedSkull)
        {
            var player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                player.GetComponentInChildren<Sword>().enabled = false;
                turnedSwordOff = true;
            }
        }
    }

    public void Click()
    {
        if (dialogBox != null) return;

        dialogBox = Instantiate(dialogBoxPrefab, transform);
        var controller = dialogBox.GetComponent<DialogBoxController>();
        if (attachedSkull.Value)
        {
            controller.SetText(finalText);
        }
        else if (pickedUpSkull.Value && !attachedSkull.Value)
        {
            var skull = Instantiate(skullPrefab, transform);
            skull.transform.position = skull.transform.position + new Vector3(0, 1, 0);
            attachedSkull.Value = true;
            dialogBox.GetComponentInChildren<DialogueBoxAnimEvents>().OnDestroyingDialogueBox += GiveSword;
            controller.SetText(thanksText);
        }
        else
        {
            controller.SetText(text);
        }
    }

    void GiveSword()
    {
        var player = GameObject.FindWithTag("Player");
        player.GetComponentInChildren<Sword>().enabled = true;
        var sword = Instantiate(swordGraphicsPrefab, player.transform);
        sword.transform.position = sword.transform.position + new Vector3(0f, 1f, 0f);
        StartCoroutine(DestroyAfter(sword));
    }

    IEnumerator DestroyAfter(GameObject someObject)
    {
        yield return new WaitForSeconds(3f);
        Destroy(someObject);
    }
}
