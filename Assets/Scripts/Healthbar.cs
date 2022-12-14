using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Image background;
    public Image healthbar;

    static Healthbar _instance;
    public static Healthbar Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<Healthbar>();
            return _instance;
        }
    }

    void Start()
    {
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        if (CharacterManager.Instance.activePlayer.GetComponent<PlayerHealth>().soHealth.Value < CharacterManager.Instance.activePlayer.GetComponent<PlayerHealth>().maxHealth)
        {
            background.enabled = true;
            healthbar.enabled = true;
        }

        if (CharacterManager.Instance.activePlayer.GetComponent<PlayerHealth>().soHealth.Value == CharacterManager.Instance.activePlayer.GetComponent<PlayerHealth>().maxHealth)
        {
            Disable();
        }
        healthbar.fillAmount = CharacterManager.Instance.activePlayer.GetComponent<PlayerHealth>().soHealth.Value / CharacterManager.Instance.activePlayer.GetComponent<PlayerHealth>().maxHealth;
    }

    void Disable()
    {
        background.enabled = false;
        healthbar.enabled = false;
    }
}
