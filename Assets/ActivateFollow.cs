using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFollow : MonoBehaviour, IClickable
{
    [SerializeField] CharacterManager characterManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Click()
    {
        characterManager.playerFollow = !characterManager.playerFollow;
    }
}
