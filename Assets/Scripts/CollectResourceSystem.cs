using UnityEngine;
using UnityEngine.EventSystems;

public class CollectResourceSystem : MonoBehaviour
{
    Player player;
    Transform highlight;
    RaycastHit raycastHit;
    public float maxDist = 2f;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        // Highlight
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
        {
            highlight = raycastHit.transform;
            if (highlight.CompareTag("Resource"))
            {
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                }
            }
            else
            {
                highlight = null;
            }
        }

        if (highlight != null && Input.GetMouseButtonDown(0))
        {
            float dist = Vector3.Distance(highlight.transform.position, player.transform.position);
            if (dist <= maxDist)
            {
                highlight.GetComponent<Resource>().Collect();
                print("Resource Collected!");
            }
        }
    }
}
