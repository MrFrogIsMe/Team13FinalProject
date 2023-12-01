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
    [SerializeField] private int collisionCount;

    //[SerializeField] float health;

    void Start()
    {
        collisionCount = 0;  
    }

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
            collisionCount++;
            cannotBuild = true;
            GetComponent<MeshRenderer>().material = blueprintMatErr;
        }
    }
    private void OnTriggerExit()
    {
        if (isBlueprint)
        {
            collisionCount--;
            if (collisionCount == 0){
                cannotBuild = false;
                GetComponent<MeshRenderer>().material = blueprintMat;
            }
        }
    }
}
