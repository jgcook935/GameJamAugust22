using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwiftClif : MonoBehaviour, IClickable
{
    [SerializeField] GameObject dialogBoxPrefab;
    [SerializeField] BoolSO hasWonRaceSO;
    [SerializeField] Animator animator;
    public Rigidbody2D rb;
    GameObject dialogBox;
    public List<string> text { get; set; }
    private bool raceStarted = false;
    private float moveSpeed = 10f;

    void Start()
    {
        //
    }

    void FixedUpdate()
    {
        if (raceStarted)
            rb.velocity = new Vector2(1 * moveSpeed, 0 * moveSpeed);
    }

    public void Click()
    {
        if (dialogBox != null) return;
        StartRace();
        dialogBox = Instantiate(dialogBoxPrefab, transform);
        dialogBox.GetComponent<DialogBoxController>().SetText(GetComponent<ISign>().text);
    }

    public void StartRace()
    {
        animator.enabled = true;
        raceStarted = true;
    }
}
