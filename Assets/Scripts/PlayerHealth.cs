using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public FloatSO soHealth;

    public float maxHealth = 200f;
    SpriteRenderer spriteRenderer;
    bool isImmune = false;

    AudioSource source => GetComponent<AudioSource>();
    [SerializeField] AudioClip[] clips;

    static PlayerHealth _instance;
    public static PlayerHealth Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<PlayerHealth>();
            return _instance;
        }
    }

    void OnDestroy()
    {
        Physics2D.IgnoreLayerCollision(8, 7, false);
        Physics2D.IgnoreLayerCollision(8, 11, false);
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
                PlayDamageSound();
                StartCoroutine(FlashSprite());                
                var damage = collision.gameObject.GetComponent<EnemyHealth>().damage;
                soHealth.Value -= damage;
                Healthbar.Instance.UpdateHealthBar();
            }
        }
        else if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            if (collision.otherCollider.CompareTag("Player"))
            {
                if (isImmune) return;
                PlayDamageSound();
                StartCoroutine(FlashSprite());
                var damage = collision.gameObject.GetComponent<Fireball>().damage;
                soHealth.Value -= damage;
                Healthbar.Instance.UpdateHealthBar();
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

    void PlayDamageSound()
    {
        // possibly check to make sure we're the main player so we can only play the sound once
        // i don't want to split this script off or anything, so it'd be nice to figure out
        // who the main player is
        var random = Random.Range(0, 3);
        Debug.Log($"playing a random sound... {random}");
        source.PlayOneShot(clips[random]);
    }
}