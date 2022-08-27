using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 200f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.otherCollider.CompareTag("Player"))
            {
                Debug.Log($"Player was touched by the enemy - DO DAMAGE STUFF HERE");
            }
            var damage = collision.gameObject.GetComponent<EnemyHealth>().damage;
            health -= damage;
        }
    }
}
