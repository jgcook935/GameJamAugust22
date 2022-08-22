using UnityEngine;

public class ClickManager : MonoBehaviour
{
    [SerializeField] Camera mainCam;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit)
            {
                IClickable clickable = hit.collider.GetComponent<IClickable>();
                clickable?.Click();
            }
        }
        else
        {
            Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            // TODO: uncomment if we hook up one of these
            // if (hit)
            // {
            //     UIManager.Instance.DoCursorHover();
            // }
            // else
            // {
            //     UIManager.Instance.DoCursorDefault();
            // }
        }
    }
}
