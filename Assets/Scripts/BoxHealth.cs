using System.Collections;
using UnityEngine;

public class BoxHealth : MonoBehaviour
{
    private float currentHealth = 50;

    public void DoDamage(Vector2 direction)
    {
        Debug.Log("Box got hit");
        currentHealth -= 10;

        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
