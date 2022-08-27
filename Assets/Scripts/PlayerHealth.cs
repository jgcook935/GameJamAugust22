using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 200f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var damage = collision.gameObject.GetComponent<EnemyHealth>().damage;
            health -= damage;
            //Debug.Log($"Player was damaged by enemy by {damage} hp points");
        }
    }
}
