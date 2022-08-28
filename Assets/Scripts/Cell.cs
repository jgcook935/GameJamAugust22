using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour, IClickable
{
    [SerializeField] GameObject dialogBoxPrefab;
    [SerializeField] Sprite openDoor;
    [SerializeField] GameObject playerThreePrefab;
    [SerializeField] GameObject keyPrefab;
    [SerializeField] BoolSO hasKeySO;
    [SerializeField] BoolSO hasPlayerThreeSO;
    GameObject dialogBox;


    public List<string> text { get; set; } = new List<string>
    {
        "Hey kid, get me outta here will ya?",
    };

    public void Click()
    {
        if (hasKeySO.Value)
        {
            OpenDoor();
        }
        else if (!hasPlayerThreeSO.Value)
        {
            if (dialogBox != null) return;
            dialogBox = Instantiate(dialogBoxPrefab, transform);
            dialogBox.GetComponent<DialogBoxController>().SetText(text);
        }
    }

    private void OpenDoor()
    {
        var activePlayerTransform = CharacterManager.Instance.activePlayer.gameObject.transform;
        var key = Instantiate(keyPrefab, activePlayerTransform);
        key.transform.position = activePlayerTransform.position + new Vector3(0f, 1f, 0f);
        StartCoroutine(DestroyAfter(key));
    }

    IEnumerator DestroyAfter(GameObject someObject)
    {
        yield return new WaitForSeconds(1f);
        Destroy(someObject);
        GetComponent<SpriteRenderer>().sprite = openDoor;
        var player3 = Instantiate(playerThreePrefab, transform);
        player3.transform.position = player3.transform.position + new Vector3(1f, -1f, 0f);
    }
}
