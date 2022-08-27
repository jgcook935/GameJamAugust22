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
        background.enabled = false;
        healthbar.enabled = false;
    }

    public void DecreaseHealth()
    {
        if (CharacterManager.Instance.activePlayer.GetComponent<PlayerHealth>().currentHealth < CharacterManager.Instance.activePlayer.GetComponent<PlayerHealth>().maxHealth)
        {
            background.enabled = true;
            healthbar.enabled = true;
        }
        healthbar.fillAmount = CharacterManager.Instance.activePlayer.GetComponent<PlayerHealth>().currentHealth / CharacterManager.Instance.activePlayer.GetComponent<PlayerHealth>().maxHealth;
    }
}
