using UnityEngine;

public class Sword : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        var direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);

        //Debug.DrawRay(Input.mousePosition, transform.position);
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log($"{Input.mousePosition.x}, {Input.mousePosition.y}, {Input.mousePosition.z}");
            //Debug.Log($"{Input.mousePosition.normalized}");

            //if (Input.mousePosition.x <= Screen.width / 2)
            //{
            //    transform.localPosition = new Vector3(-0.3f, 0, 0f);
            //    transform.localScale = new Vector3(1, 1, 1);
            //    Debug.Log("left");
            //}
            //else if (Input.mousePosition.x >= Screen.width / 2)
            //{
            //    transform.localPosition = new Vector3(0.3f, 0f, 0f);
            //    transform.localScale = new Vector3(-1, 1, 1);
            //    Debug.Log("right");
            //}

            //var direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            //var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //_transform.rotation = Quaternion.Slerp(_transform.rotation, rotation, 5 * Time.deltaTime);

            animator.Play("Swingtinith");
        }
    }
}
