using UnityEngine;
using System.Collections;

public class ArrowDamage : MonoBehaviour
{
    void Awake()
    {
        StartCoroutine(DestroyAfter());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.gameObject.GetComponent<EnemyHealth>();    
        Destroy(gameObject);
        if (enemy != null)
        {
            enemy.DoDamage(transform.position);
        }
    }

    IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
