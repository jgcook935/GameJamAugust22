using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth = 200f;
    public float maxHealth = 200f;
    SpriteRenderer spriteRenderer;
    bool isImmune = false;

    static PlayerHealth _instance;
    public static PlayerHealth Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<PlayerHealth>();
            return _instance;
        }
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.otherCollider.CompareTag("Player"))
            {
                if (isImmune) return;
                StartCoroutine(FlashSprite());                
                var damage = collision.gameObject.GetComponent<EnemyHealth>().damage;
                currentHealth -= damage;
                Healthbar.Instance.DecreaseHealth();
            }
        }
        else if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            if (collision.otherCollider.CompareTag("Player"))
            {
                if (isImmune) return;
                StartCoroutine(FlashSprite());
                var damage = collision.gameObject.GetComponent<Fireball>().damage;
                currentHealth -= damage;
                Healthbar.Instance.DecreaseHealth();
            }
        }
    }

    IEnumerator FlashSprite()
    {
        isImmune = true;
        for (int i = 0; i < 6; i++)
        {
            spriteRenderer.enabled = false;
            Physics2D.IgnoreLayerCollision(8, 7, true);
            Physics2D.IgnoreLayerCollision(8, 11, true);
            yield return new WaitForSeconds(.25f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.25f);
            Physics2D.IgnoreLayerCollision(8, 7, false);
            Physics2D.IgnoreLayerCollision(8, 11, false);
        }
        isImmune = false;
    }
}