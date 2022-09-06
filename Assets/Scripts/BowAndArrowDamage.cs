using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class BowAndArrowDamage : Ability
{
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject arrowPrefab;
    public Animator animator;

    Vector2 mousePos;
    Vector2 lookDir;
    float lookAngle;
    float arrowForce = 3.5f;
    bool isShooting = false;

    void Update()
    {
        // not sure if we'll need this or not
        //if (EventSystem.current.IsPointerOverGameObject()) return;

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && CharacterManager.Instance.activePlayer.GetComponentInChildren<BowAndArrow>() != null)
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        lookDir = mousePos - (Vector2)firePoint.position;
        lookAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        //animator.SetFloat("MouseAngleX", lookDir.x);
        //animator.SetFloat("MouseAngleY", lookDir.y);
    }

    void Shoot()
    {
        if (isShooting || !Enabled) return;
        isShooting = true;
        animator.Play("Fire");
        StartCoroutine(ShootArrow());
    }

    // this ins't actually doing a coroutine because it's called from update
    IEnumerator ShootArrow()
    {
        yield return new WaitForSeconds(0.3f);
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.Euler(0f, 0f, lookAngle));
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.AddForce(lookDir * arrowForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.3f);
        isShooting = false;
    }
}
