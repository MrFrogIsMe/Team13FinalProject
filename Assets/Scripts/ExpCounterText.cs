using UnityEngine;
using Text = TMPro.TextMeshProUGUI;


public class ExpCounterText : MonoBehaviour
{
    Text expCountText;
    // Start is called before the first frame update
    void Start()
    {
        expCountText = GetComponent<Text>();
        expCountText.text = "exp = 0";
        expCountText.text += "\n";
        expCountText.text += "lvl = 0";
    }

    // Update is called once per frame
    void Update()
    {
        expCountText.text = "exp = " + ExperienceSystem.Instance.GetExperience().ToString();
        expCountText.text += "\n";
        expCountText.text += "lvl = " + ExperienceSystem.Instance.GetLevel().ToString();
    }
}
