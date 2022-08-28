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
        if (enemy != null)
        {
            enemy.DoDamage(transform.position);
        }

        var dragon = collision.gameObject.GetComponent<DragonHealth>();
        if (dragon != null)
        {
            dragon.DoDamage();
        }
        Destroy(gameObject);
    }

    IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
