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
    [SerializeField] GameObject playerThreePrefab;
    [SerializeField] GameObject mainCameraPrefab;
    [SerializeField] BoolSO hasPlayerTwoSO;
    [SerializeField] BoolSO hasPlayerThreeSO;

    public IntSO activePlayerSO;

    private PlayerMovement player1;
    private PlayerMovement player2;
    private PlayerMovement player3;
    private CameraController cameraController;

    private List<PlayerMovement> players = new List<PlayerMovement>();
    private LinkedList<Vector3> activePlayerPosition = new LinkedList<Vector3>();
    private const int maxLinkedListCount = 30;

    void Start()
    {
        // make camera and player1
        cameraController = Instantiate(mainCameraPrefab, transform).GetComponent<CameraController>();
        player1 = Instantiate(playerOnePrefab, transform).GetComponent<PlayerMovement>();
        players.Add(player1);

        var sceneLocation = ChangeScenesManager.Instance.GetSceneLocation();
        if (sceneLocation != Vector2.zero) player1.transform.position = sceneLocation;

        if (hasPlayerTwoSO.Value) AddPlayer(Instantiate(playerTwoPrefab, transform).GetComponent<PlayerMovement>());
        if (hasPlayerThreeSO.Value) AddPlayer(Instantiate(playerThreePrefab, transform).GetComponent<PlayerMovement>());

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
        var elementsBehindPlayer = 10;
        if (activePlayerSO.Value == 2) elementsBehindPlayer = 5;

        foreach (var player in players)
        {
            if (player != activePlayer)
            {
                if (activePlayerPosition.Count > elementsBehindPlayer * followerIndex)
                {
                    if (!player.enabled)
                    {
                        player.transform.position = activePlayerPosition.ElementAt(elementsBehindPlayer * followerIndex);
                        player.animator.SetFloat("Horizontal", activePlayer.movement.x);
                        player.animator.SetFloat("Vertical", activePlayer.movement.y);
                        player.animator.SetFloat("Speed", activePlayer.movement.sqrMagnitude);
                        player.animator.SetBool("IsPlayerTwo", false);
                    }
                }
                followerIndex++;
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
            player2.gameObject.layer = 8;
        }
        else if (!player3)
        {
            players.Add(newPlayer);
            player3 = newPlayer;
            player3.transform.position = player1.transform.position;
            player3.gameObject.layer = 8;
        }
        else Debug.LogError("No New Player Slots");
    }

    void RotatePlayers()
    {
        if (activePlayer == player1 && player2) SetActivePlayerIndex(2);
        else if (player2 && activePlayer == player2 && player3) SetActivePlayerIndex(3);
        else if (player2 && activePlayer == player2 && !player3) SetActivePlayerIndex(1);
        else if (player3 && activePlayer == player3) SetActivePlayerIndex(1);
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
                    if (player3 != null) player3.enabled = false;
                    activePlayerSO.Value = 2;
                    break;
                }
            case 3:
                {
                    player3.isActivePlayer = true;
                    player3.enabled = true;
                    activePlayer = player3;
                    player1.enabled = false;
                    player2.enabled = false;
                    activePlayerSO.Value = 3;
                    break;
                }
            default:
                {
                    player1.isActivePlayer = true;
                    player1.enabled = true;
                    activePlayer = player1;
                    activePlayerSO.Value = 1;
                    if (player2 != null) player2.enabled = false;
                    if (player3 != null) player3.enabled = false;
                    break;
                }
        }

        cameraController.target = activePlayer.transform;
    }
}
