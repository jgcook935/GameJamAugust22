using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;
    public float damage = 10f;

    void DamageEnemy(float damage)
    {
        health -= damage;
        System.Console.WriteLine($"enemy damaged by {damage}");
    }

    // TODO: make this do something
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Weapon"))
    //    {
    //        var damage = collision.gameObject.GetComponent<WeaponDamage>().damage;
    //        health -= damage;
    //        Debug.Log($"Enemy was damaged by player by {damage} hp points");
    //    }
    //}
}
