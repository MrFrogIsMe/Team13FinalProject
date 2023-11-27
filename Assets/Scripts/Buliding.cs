using System;
using System.Collections.Generic;
using UnityEngine;

public class Buliding : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Material blueprintMat;
    [SerializeField] private Material blueprintMatErr;
    [SerializeField] private Material buildingMat;
    

    [Header("State")]
    public bool cannotBuild;
    [SerializeField] private bool isBlueprint;

    //[SerializeField] float health;

    public void SetAsBlueprint() { 
        isBlueprint = true;
        GetComponent<Collider>().isTrigger = true;
        GetComponent<MeshRenderer>().material = blueprintMat;
    }
    public void SetAsBuilding(){
        isBlueprint = false;
        GetComponent<Collider>().isTrigger = false;
        GetComponent<MeshRenderer>().material = buildingMat;
    }

    private void OnTriggerEnter() {
        if (isBlueprint)
        {
            cannotBuild = true;
            GetComponent<MeshRenderer>().material = blueprintMatErr;
        }
    }
    private void OnTriggerExit()
    {
        Debug.Log(isBlueprint);
        if (isBlueprint)
        {
            cannotBuild = false;
            GetComponent<MeshRenderer>().material = blueprintMat;
        }
    }
}
