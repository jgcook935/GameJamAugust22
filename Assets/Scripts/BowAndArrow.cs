using UnityEngine;

public class BowAndArrow : MonoBehaviour
{
    Animator animator;

    void Awake()
    {
        // animator = GetComponent<Animator>();
    }

    void Update()
    {
        var direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);

        if (Input.GetMouseButtonDown(0) && CharacterManager.Instance.activePlayer.GetComponentInChildren<BowAndArrow>() != null)
        {
            // animator.Play("Fire");
        }
    }
}
