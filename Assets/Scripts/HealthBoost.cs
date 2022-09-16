using System.Collections;
using UnityEngine;

public class HealthBoost : MonoBehaviour
{
    AudioSource source => GetComponent<AudioSource>();
    SpriteRenderer sr => GetComponent<SpriteRenderer>();

    void OnTriggerEnter2D()
    {
        source.Play();
        sr.enabled = false;
        PlayerHealth.Instance.soHealth.Value = PlayerHealth.Instance.maxHealth;
        Healthbar.Instance.UpdateHealthBar();
        StartCoroutine(DestroyAfter(gameObject));
    }

    IEnumerator DestroyAfter(GameObject someObject)
    {
        yield return new WaitForSeconds(2f);
        Destroy(someObject);
        CharacterManager.Instance.SetControlsEnabled(true);
    }
}
