using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwiftClif : MonoBehaviour, IClickable
{
    [SerializeField] GameObject dialogBoxPrefab;
    [SerializeField] BoolSO hasWonRaceSO;
    [SerializeField] BoolSO hasKeySO;
    [SerializeField] BoolSO hasAttemptedRace;
    [SerializeField] Animator animator;
    [SerializeField] GameObject key;
    AudioSource source => GetComponent<AudioSource>();
    public AudioClip start;
    public AudioClip clap;
    public AudioClip keyReveal;
    public Rigidbody2D rb;
    GameObject dialogBox;
    public bool raceStarted = false;
    private float moveSpeed = 9.5f;

    public List<string> text { get; set; } = new List<string>
    {
        "They call me Swift Clif",
        "Ain't nobody faster than me",
        "Care for a race? Win and I'll give you my shiny key",
        "Ready? GO!!!"
    };

    public List<string> lostText { get; set; } = new List<string>
    {
        "Too Slow!",
    };

    public List<string> wonText { get; set; } = new List<string>
    {
        "What! No fair!",
    };

    public List<string> finalText { get; set; } = new List<string>
    {
        "I'm just having an off day!",
    };

    void Start()
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
        if (hasKeySO.Value) Destroy(key);
    }

    void FixedUpdate()
    {
        if (raceStarted)
            rb.velocity = new Vector2(1 * moveSpeed, 0 * moveSpeed);
    }

    public void Click()
    {
        if (dialogBox != null) return;
        dialogBox = Instantiate(dialogBoxPrefab, transform);

        if (hasWonRaceSO.Value)
        {
            dialogBox.GetComponent<DialogBoxController>().SetText(finalText);
            if (!hasKeySO.Value) dialogBox.GetComponentInChildren<DialogueBoxAnimEvents>().OnDestroyingDialogueBox += GiveKey;
        }
        else
        {
            dialogBox.GetComponent<DialogBoxController>().SetText(text);
            dialogBox.GetComponentInChildren<DialogueBoxAnimEvents>().OnDestroyingDialogueBox += StartRace;
        }

    }

    public void StartRace()
    {
        gameObject.layer = LayerMask.NameToLayer("NPC that player can walk through");
        hasAttemptedRace.Value = true;
        GetComponent<Rigidbody2D>().isKinematic = false;
        animator.enabled = true;
        raceStarted = true;
        source.PlayOneShot(start);
    }

    public void FinishRace(Collider2D finishCollision)
    {
        Debug.Log($"Finish Collision {finishCollision.gameObject.name}");
        var playerWon = finishCollision.gameObject.name != "Swift Clif";
        hasWonRaceSO.Value = playerWon;
        if (dialogBox != null) return;
        dialogBox = Instantiate(dialogBoxPrefab, transform);
        dialogBox.GetComponent<DialogBoxController>().SetText(playerWon ? wonText : lostText);
        dialogBox.GetComponentInChildren<DialogueBoxAnimEvents>().OnDestroyingDialogueBox += FinishedRaceDialogue;
        source.PlayOneShot(clap);
    }

    private void FinishedRaceDialogue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void GiveKey()
    {
        hasKeySO.Value = true;
        source.PlayOneShot(keyReveal);
        CharacterManager.Instance.SetControlsEnabled(false);
        var playerTransform = CharacterManager.Instance.activePlayer.gameObject.transform;
        key.transform.SetParent(playerTransform);
        key.transform.position = playerTransform.position + new Vector3(0f, 1f, 0f);
        StartCoroutine(DestroyAfter(key));
    }

    IEnumerator DestroyAfter(GameObject someObject)
    {
        yield return new WaitForSeconds(2f);
        Destroy(someObject);
        CharacterManager.Instance.SetControlsEnabled(true);
    }
}
