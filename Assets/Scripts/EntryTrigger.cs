using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EntryTrigger : MonoBehaviour
{
    [SerializeField] int sceneIndex;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player") return;
        LoadScene();
    }

    void LoadScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
