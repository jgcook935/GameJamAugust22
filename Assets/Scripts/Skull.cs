using UnityEngine;

public class Skull : MonoBehaviour, IClickable
{
    [SerializeField] BoolSO pickedUpSkull;
    [SerializeField] BoolSO attachedSkull;
    [SerializeField] GameObject skullPrefab;

    AudioSource source => GetComponent<AudioSource>();

    private GameObject skull;

    void Start()
    {
        if (!pickedUpSkull.Value) skull = Instantiate(skullPrefab, transform);
    }

    public void Click()
    {
        if (attachedSkull.Value) return;
        else
        {
            source.Play();
            pickedUpSkull.Value = true;
            Destroy(skull);
        }
    }
}
