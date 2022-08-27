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
    [SerializeField] GameObject mainCameraPrefab;
    [SerializeField] BoolSO hasPlayerTwoSO;
    [SerializeField] IntSO activePlayerSO;


    private PlayerMovement player1;
    private PlayerMovement player2;
    private CameraController cameraController;

    private List<PlayerMovement> players = new List<PlayerMovement>();
    private LinkedList<Vector3> activePlayerPosition = new LinkedList<Vector3>();
    private const int maxLinkedListCount = 10;

    private int playerCount = 0;

    void Start()
    {
        // make camera and player1
        cameraController = Instantiate(mainCameraPrefab, transform).GetComponent<CameraController>();
        player1 = Instantiate(playerOnePrefab, transform).GetComponent<PlayerMovement>();
        players.Add(player1);

        var sceneLocation = ChangeScenesManager.Instance.GetSceneLocation();
        if (sceneLocation != Vector2.zero) player1.transform.position = sceneLocation;

        if (hasPlayerTwoSO.Value) AddPlayer(Instantiate(playerTwoPrefab, transform).GetComponent<PlayerMovement>());

        SetActivePlayerIndex(activePlayerSO.Value);

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

        if (Input.GetKeyDown(KeyCode.Tab))
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
            player2.transform.position = player1.transform.position;
        }
    }

    void RotatePlayers()
    {
        if (activePlayer == player1) SetActivePlayerIndex(2);
        else SetActivePlayerIndex(1);

    }

    private void SetActivePlayerIndex(int index)
    {
        switch (index)
        {
            case 2:
                {
                    player2.isActivePlayer = true;
                    player2.enabled = true;
                    activePlayer = player2;
                    player1.enabled = false;
                    activePlayerSO.Value = 2;
                    break;
                }
            default:
                {
                    player1.isActivePlayer = true;
                    player1.enabled = true;
                    activePlayer = player1;
                    activePlayerSO.Value = 1;
                    if (player2 != null) player2.enabled = false;
                    break;
                }
        }

        cameraController.target = activePlayer.transform;
    }
}
