using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] PlayerMovement player1;
    [SerializeField] PlayerMovement player2;
    [SerializeField] PlayerMovement player3;

    public bool followPlayer2;


    LinkedList<Vector3> currentPlayerPositions = new LinkedList<Vector3>();
    int maxLinkedListCount = 5; // 5 per player is probably good

    void Start()
    {
        currentPlayerPositions.AddFirst(player1.gameObject.transform.position);
    }

    void Update()
    {
        if(player1.transform.position != currentPlayerPositions.First())
        {
            if (currentPlayerPositions.Count >= maxLinkedListCount)
                currentPlayerPositions.RemoveLast();
            currentPlayerPositions.AddFirst(player1.gameObject.transform.position);

        }
        if (followPlayer2 && currentPlayerPositions.Count > 3)
        {
            player2.transform.position = currentPlayerPositions.ElementAt(4);

            player2.animator.SetFloat("Horizontal", player1.movement.x);
            player2.animator.SetFloat("Vertical", player1.movement.y);
            player2.animator.SetFloat("Speed", player1.movement.sqrMagnitude);
        }
    }

    public void AddPlayer(PlayerMovement newPlayer)
    {
        if (!player2)
        {
            player2 = newPlayer;
            followPlayer2 = true;
        }
        else if (!player3)
        {
            player3 = newPlayer;
        }

    }
}
