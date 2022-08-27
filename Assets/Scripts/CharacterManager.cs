using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    static CharacterManager _instance;
    public static CharacterManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<CharacterManager>();
            return _instance;
        }
    }

    private PlayerMovement player1;
    private PlayerMovement player2;

    [HideInInspector] public PlayerMovement activePlayer;

    [SerializeField] GameObject playerOnePrefab;
    [SerializeField] GameObject playerTwoPrefab;

    [SerializeField] BoolSO hasPlayerTwoSO;

    [SerializeField] GameObject mainCameraPrefab;

    private CameraController cameraController;

    LinkedList<Vector3> currentPlayerPositions = new LinkedList<Vector3>();
    int maxLinkedListCount = 5; // 5 per player is probably good

    void Start()
    {
        cameraController = Instantiate(mainCameraPrefab, transform).GetComponent<CameraController>();

        player1 = Instantiate(playerOnePrefab, transform).GetComponent<PlayerMovement>();

        if (hasPlayerTwoSO.Value)
        {
            var newPlayer = Instantiate(playerTwoPrefab, transform);
            newPlayer.transform.position = player1.transform.position;
            player2 = newPlayer.GetComponent<PlayerMovement>();
        }

        activePlayer = player1; // todo use BoolSO

        cameraController.target = activePlayer.transform;

        currentPlayerPositions.AddFirst(activePlayer.transform.position);
    }

    void Update()
    {
        if (player1.transform.position != currentPlayerPositions.First())
        {
            if (currentPlayerPositions.Count >= maxLinkedListCount)
                currentPlayerPositions.RemoveLast();
            currentPlayerPositions.AddFirst(player1.gameObject.transform.position);
        }

        if (currentPlayerPositions.Count > 4)
        {
            if (player2 != null)
            {
                player2.transform.position = currentPlayerPositions.ElementAt(4);
                player2.animator.SetFloat("Horizontal", player1.movement.x);
                player2.animator.SetFloat("Vertical", player1.movement.y);
                player2.animator.SetFloat("Speed", player1.movement.sqrMagnitude);
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            RotatePlayers();
        }
    }

    public void AddPlayer(PlayerMovement newPlayer)
    {
        if (!player2)
        {
            player2 = newPlayer;
        }
        //else if (!player3)
        //{
        //    player3 = newPlayer;
        //}
    }

    void RotatePlayers()
    {
        PlayerMovement playerTemp = player1;
        player1 = player2;
        player2 = playerTemp;

        if (player1) player1.enabled = true;
        if (player2) player2.enabled = false;

        cameraController.target = player1.gameObject.transform;
    }
}
