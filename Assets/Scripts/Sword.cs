using UnityEngine;

public class Sword : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        var direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            animator.Play("Swingtinith");
        }
    }
}
