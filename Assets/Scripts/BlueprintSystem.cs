using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class BlueprintSystem : MonoBehaviour
{
    [SerializeField] private List<GameObject> buildings;
    public bool showHotBar = false;
    // Start is called before the first frame update
    void Start()
    {
        int length = buildings.Count;
        for (int i = 0; i < length; i++) { 
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) {
            if (showHotBar) {

            }
            else { 

            }
        
        }
    }
}
