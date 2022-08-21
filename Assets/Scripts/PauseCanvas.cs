using UnityEngine;

public class PauseCanvas : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            canvasGroup.alpha = 1;
            // TODO: lock character movement
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeGame();
        }
    }

    public void ResumeGame()
    {
        Debug.Log("Resume button pressed. Resuming.");
        canvasGroup.alpha = 0;
    }

    public void QuitGame()
    {
        Debug.Log("Quit button pressed. Quitting.");
        Application.Quit();
    }
}
