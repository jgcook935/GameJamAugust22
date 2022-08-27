using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private float currentHealth;
    public float damage;

    private Rigidbody2D rb;
    private float strength;
    private float delay = .15f;

    private void Awake()
    {
        var random = Random.Range(2, 7);
        currentHealth = 10 * random;
        damage = 5 * random;
        strength = 40 / random;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void DoDamage(Vector2 direction)
    {
        PlayKnockback(direction);
        currentHealth -= 10;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void PlayKnockback(Vector2 enemyDirection)
    {
        StopAllCoroutines();
        var direction = ((Vector2)transform.position - enemyDirection).normalized;
        rb.AddForce(direction * strength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector2.zero;
    }
}
