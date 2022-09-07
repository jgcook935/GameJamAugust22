using System.Collections;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    [SerializeField] GameObject fireballPrefab;
    [SerializeField] Transform firePoint;

    [SerializeField] AudioSource source;
    [SerializeField] AudioClip fireballClip;

    Vector2 playerPos;
    Vector2 lookDir;
    float lookAngle;
    float fireballForce = .5f;

    void Start()
    {
        var random = Random.Range(5, 10);
        InvokeRepeating(nameof(Fire), random, random);
    }

    void Update()
    {
        playerPos = CharacterManager.Instance.activePlayer.transform.position;
        lookDir = playerPos - (Vector2)firePoint.position;
        if (lookDir.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        lookAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
    }

    void Fire()
    {
        StartCoroutine(FireOnInterval());
    }

    IEnumerator FireOnInterval()
    {
        var random = Random.Range(0, 10);
        yield return new WaitForSeconds(random);
        var fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.Euler(0f, 0f, lookAngle));
        var rb = fireball.GetComponent<Rigidbody2D>();
        rb.velocity = lookDir * fireballForce;
        source.PlayOneShot(fireballClip);
        yield return new WaitForSeconds(0.3f);
        // instantiate the prefab and launch it toward the player
    }
}
