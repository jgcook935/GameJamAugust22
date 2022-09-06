using UnityEngine;

public class Sword : Ability
{
    Animator animator;
    AudioSource source;
    public AudioClip[] clips;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        var direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);

        if (Enabled && Input.GetMouseButtonDown(0) && CharacterManager.Instance.activePlayer.GetComponentInChildren<Sword>() != null)
        {
            animator.Play("Swingtinith");
            source.clip = clips[Random.Range(0, 5)];
            if (source.isPlaying) source.Stop();
            source.Play();
        }
    }
}
