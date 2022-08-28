using System.Collections;
using UnityEngine;

public class DragonHealth : MonoBehaviour
{
    private float currentHealth;
    public float damage;

    public AudioSource source;
    public AudioClip[] death;
    BoxCollider2D boxCollider;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        var random = Random.Range(2, 7);
        currentHealth = 10 * random;
        damage = 5 * random;

        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DoDamage()
    {
        currentHealth -= 10;

        if (currentHealth <= 0)
        {
            StartCoroutine(DoDeath());
            if (SlimeWaveFinal.Instance) SlimeWaveFinal.Instance.DecrementDragonCount();
        }
    }

    IEnumerator DoDeath()
    {
        boxCollider.enabled = false;
        spriteRenderer.enabled = false;
        source.PlayOneShot(death[Random.Range(0, 2)]);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
