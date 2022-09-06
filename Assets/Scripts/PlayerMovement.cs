using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float tileScale = 1; //0.15f;
    public Vector2 movement;

    public float moveSpeed = 10f;
    public Rigidbody2D rb;
    public Animator animator;

    private bool canMove = true;

    [HideInInspector] public bool isActivePlayer = false;

    [SerializeField] float sprintMultiplier = 1.5f;

    static PlayerMovement _instance;
    public static PlayerMovement Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<PlayerMovement>();
            return _instance;
        }
    }

    void FixedUpdate()
    {
        if (!canMove) return;

        // handle movement
        var totalMoveSpeed = moveSpeed;
        if (CharacterManager.Instance.activePlayerSO.Value == 2)
        {
            animator.SetBool("IsPlayerTwo", true);
            totalMoveSpeed *= sprintMultiplier;
        }
        else animator.SetBool("IsPlayerTwo", false);

        var moveDirection = movement.normalized;
        rb.velocity = new Vector2(moveDirection.x * totalMoveSpeed * tileScale, moveDirection.y * totalMoveSpeed * tileScale);
    }

    void Update()
    {
        if (!canMove) return;

        // handle input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    public void SetMovementEnabled(bool enabled)
    {
        if (enabled)
        {
            canMove = true;
        }
        else
        {
            canMove = false;
            rb.velocity = Vector2.zero;
            animator.StopPlayback();
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 0);
            animator.SetFloat("Speed", 0);
        }
    }
}
