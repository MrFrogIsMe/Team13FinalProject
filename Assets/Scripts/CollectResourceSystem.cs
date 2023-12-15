using UnityEngine;
using UnityEngine.EventSystems;
using Text = TMPro.TextMeshProUGUI;

public class CollectResourceSystem : MonoBehaviour
{
    Player player;
    Transform highlight;
    Outline outline;
    RaycastHit raycastHit;
    public Text resourceCnt;

    public float maxDist = 2f;

    void Start()
    {
        player = FindObjectOfType<Player>();
        //UpdateResourceText();
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
        // !EventSystem... is for not clicking through UI
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
        {
            highlight = raycastHit.transform;
            if (highlight.CompareTag("Resource"))
            {
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    outline = highlight.gameObject.GetComponent<Outline>();
                    outline.OutlineWidth = 7f;
                    outline.enabled = true;
                }
                else
                {
                    outline = highlight.gameObject.AddComponent<Outline>();
                    outline.OutlineWidth = 7f;
                    outline.enabled = true;
                }
            }
            else
            {
                highlight = null;
            }
        }

        if (highlight != null)
        {
            float dist = Vector3.Distance(highlight.transform.position, player.transform.position);
            if (dist < maxDist)
            {
                outline.OutlineColor = Color.green;
            }
            else
            {
                outline.OutlineColor = Color.red;
            }
            outline.UpdateMaterialProperties();
        }
    }

    public bool Collectable()
    {
        if (highlight == null)
            return false;
        float dist = Vector3.Distance(highlight.transform.position, player.transform.position);
        return dist < maxDist;
    }

    public void Collect()
    {
        Resource resource = highlight.GetComponent<Resource>();
        resource.Collect();
        player.resources[resource.resourceType] += 1;

        //UpdateResourceText();
    }
    
    /*void UpdateResourceText()
    {
        resourceCnt.text = "";
        foreach (var resource in player.resources)
        {
            resourceCnt.text += resource.Key + " x " + resource.Value.ToString() + "\n";
        }
    }*/
}
