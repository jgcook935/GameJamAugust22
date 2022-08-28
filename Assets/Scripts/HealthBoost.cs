using UnityEngine;

public class HealthBoost : MonoBehaviour
{
    void OnTriggerEnter2D()
    {
        PlayerHealth.Instance.soHealth.Value = PlayerHealth.Instance.maxHealth;
        Healthbar.Instance.UpdateHealthBar();
        Destroy(this.gameObject);
    }
}
