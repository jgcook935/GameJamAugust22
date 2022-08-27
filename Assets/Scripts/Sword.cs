using UnityEngine;

public class Sword : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        var enemy = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.DoDamage(transform.position);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.Play("Swing");
        }
    }
}
