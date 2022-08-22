using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBoxController : MonoBehaviour, IClickable
{
    bool isOpen = false;
    int textIndex = 0;
    List<string> text = new List<string>();

    Animator animator => GetComponentInChildren<Animator>();

    void Awake()
    {
        isOpen = true;
        // PlayerMovement.Instance.SetCanMove(false);
    }

    void OnDestroy()
    {
        // PlayerMovement.Instance.SetCanMove(true);
    }

    void Update()
    {
        NextTextOrDestroy();
    }

    public void Click()
    {
        NextTextOrDestroy();
    }

    void NextTextOrDestroy()
    {
        if (text.Count == 0)
        {
            throw new System.Exception("You need to call SetText on the DialogBoxController ya knucklehead!");
        }

        // allowing escape to destroy anyway for testing
        if (isOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            animator.SetTrigger("DialogueClosed");
        }
        else if (isOpen && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) && textIndex < text.Count)
        {
            gameObject.GetComponentInChildren<Text>().text = text[textIndex];
            textIndex++;
            // play a continue sound
        }
        else if (isOpen && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) && textIndex == text.Count)
        {
            animator.SetTrigger("DialogueClosed");
            // maybe play a dismiss sound
        }
    }

    public void SetText(List<string> text)
    {
        this.text = text;
        gameObject.GetComponentInChildren<Text>().text = this.text[textIndex];
        textIndex++;
    }
}