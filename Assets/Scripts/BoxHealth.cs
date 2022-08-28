using System.Collections;
using UnityEngine;

public class BoxHealth : MonoBehaviour
{
    private float currentHealth = 50;
    [SerializeField] BoolSO destroyedBoxes;

    void Start()
    {
        if (destroyedBoxes.Value) Destroy(this.gameObject);
    }

    public void DoDamage(Vector2 direction)
    {
        Debug.Log("Box got hit");
        currentHealth -= 10;

        if (currentHealth <= 0)
        {
            destroyedBoxes.Value = true;
            Destroy(this.gameObject);
        }
    }
}
