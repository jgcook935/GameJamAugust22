using UnityEngine.SceneManagement;
using UnityEngine;

public class TitleCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        Debug.Log("Start button pressed. Starting.");
        SceneManager.LoadScene("DungeonScene1");
    }

    public void QuitGame()
    {
        Debug.Log("Quit button pressed. Quitting.");
        Application.Quit();
    }
}
