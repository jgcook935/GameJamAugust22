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


    [HideInInspector] public PlayerMovement activePlayer;

    [SerializeField] GameObject playerOnePrefab;
    [SerializeField] GameObject playerTwoPrefab;

    [SerializeField] BoolSO hasPlayerTwoSO;

    [SerializeField] FloatSO activePlayerSO;

    [SerializeField] GameObject mainCameraPrefab;

    private PlayerMovement player1;
    private PlayerMovement player2;
    private CameraController cameraController;

    private List<PlayerMovement> players = new List<PlayerMovement>();
    private LinkedList<Vector3> activePlayerPosition = new LinkedList<Vector3>();
    private const int maxLinkedListCount = 10;

    private int playerCount = 0;

    void Start()
    {
        cameraController = Instantiate(mainCameraPrefab, transform).GetComponent<CameraController>();

        player1 = Instantiate(playerOnePrefab, transform).GetComponent<PlayerMovement>();
        players.Add(player1);

        if (hasPlayerTwoSO.Value)
        {
            var newPlayer = Instantiate(playerTwoPrefab, transform);
            newPlayer.transform.position = player1.transform.position;
            player2 = newPlayer.GetComponent<PlayerMovement>();
            players.Add(player2);
        }

        if(activePlayerSO.Value == 2)
        {
            player2.isActivePlayer = true;
            player2.enabled = true;
            activePlayer = player2;
            player1.enabled = false;
            activePlayerSO.Value = 2;
        }
        else
        {
            player1.isActivePlayer = true;
            player1.enabled = true;
            activePlayer = player1;
            if(player2 != null) player2.enabled = false;
            activePlayerSO.Value = 1;
        }

        cameraController.target = activePlayer.transform;

        activePlayerPosition.AddFirst(activePlayer.transform.position);
    }

    void Update()
    {
        if (activePlayer.transform.position != activePlayerPosition.First())
        {
            if (activePlayerPosition.Count >= maxLinkedListCount)
                activePlayerPosition.RemoveLast();
            activePlayerPosition.AddFirst(activePlayer.gameObject.transform.position);
        }

        var followerIndex = 1;
        foreach (var player in players)
        {
            if (activePlayerPosition.Count > 4 * followerIndex)
            {
                if (!player.enabled)
                {
                    player.transform.position = activePlayerPosition.ElementAt(4 * followerIndex);
                    player.animator.SetFloat("Horizontal", activePlayer.movement.x);
                    player.animator.SetFloat("Vertical", activePlayer.movement.y);
                    player.animator.SetFloat("Speed", activePlayer.movement.sqrMagnitude);
                }
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
            players.Add(newPlayer);
            player2 = newPlayer;
        }
    }

    void RotatePlayers()
    {
        if (activePlayer == player1)
        {
            player2.isActivePlayer = true;
            player2.enabled = true;
            activePlayer = player2;
            player1.enabled = false;
            activePlayerSO.Value = 2;
        }
        else
        {
            player1.isActivePlayer = true;
            player1.enabled = true;
            activePlayer = player1;
            player2.enabled = false;
            activePlayerSO.Value = 1;
        }

        cameraController.target = activePlayer.transform;
    }
}
