using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public float health = 200f;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.otherCollider.CompareTag("Player"))
            {
                StartCoroutine(FlashSprite());
            }
            var damage = collision.gameObject.GetComponent<EnemyHealth>().damage;
            health -= damage;
        }
    }

    IEnumerator FlashSprite()
    {
        boxCollider.enabled = false;
        for (int i = 0; i < 6; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.25f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.25f);
        }
        boxCollider.enabled = true;
    }
}