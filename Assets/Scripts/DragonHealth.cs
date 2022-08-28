using UnityEngine;

public class DragonHealth : MonoBehaviour
{
    private float currentHealth;
    public float damage;

    private void Awake()
    {
        var random = Random.Range(2, 7);
        currentHealth = 10 * random;
        damage = 5 * random;
    }

    public void DoDamage()
    {
        currentHealth -= 10;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            if (SlimeWaveFinal.Instance) SlimeWaveFinal.Instance.DecrementDragonCount();
        }
    }
}
