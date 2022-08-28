using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.DoDamage(transform.position);
        }
        var box = collision.gameObject.GetComponent<BoxHealth>();
        if (box != null)
        {
            box.DoDamage(transform.position);
        }
    }
}
