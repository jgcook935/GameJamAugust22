using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float damage;

    private void Awake()
    {
        damage = Random.Range(9, 21);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // doing damage on player instead
        //var player = collision.gameObject.GetComponent<PlayerHealth>();
        //if (player != null)
        //{
        //    player.currentHealth -= 20;
        //}

        Destroy(this.gameObject);
    }
}
