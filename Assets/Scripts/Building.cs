using System.Collections.Generic;
using UnityEngine;
using System;

public class Building : Entity
{
    [Header("Config")]
    [SerializeField] private Material blueprintMat;
    [SerializeField] private Material blueprintMatErr;
    [SerializeField] private Material blueprintMatGray;
    [SerializeField] private Material buildingMat;


    [Header("State")]
    public bool cannotBuild;
    public bool enoughResource;
    [SerializeField] private bool isBlueprint;
    [SerializeField] private int collisionCount;

    [Header("Buliding Stat")]
    [SerializeField] float MaxHealth = 100;
    [SerializeField] float healthPoint = 100;

    [Header("Recipe")]
    [SerializeField] List<String> resources;
    [SerializeField] List<int> quantity;

    public Dictionary<String, int> getRecipe() { 
        Dictionary<String,int> rep = new Dictionary<String, int>();
        for (int i = 0; i < resources.Count; i++) {
                rep.Add(resources[i], quantity[i]);
        }
        return rep;
    }

    void Start()
    {
        healthBar.SetMaxHealth((int)MaxHealth);
        collisionCount = 0;

        int r = resources.Count;
        int q = quantity.Count;
        if (r != q) {
            if (r > q) {
                for (int i = q - 1; i < r; i++) {
                    quantity.Add(0);
                }
            }
            else {
                for (int i = r - 1; i < q; i++)
                {
                    resources.Add("Resource");
                }
            }
        }
    }

    public override void Move() { }
    public override void Attack() { }
    public override void Die() { }


    public void setMaxHealth(float hp)
    {
        MaxHealth = hp;
        healthPoint = hp;
        healthBar.SetMaxHealth((int)hp);
    }

    void setHealth(float hp)
    {
        healthPoint = hp >= MaxHealth ? MaxHealth : hp;
    }

    public void SetAsBlueprint()
    {
        isBlueprint = true;
        GetComponent<Collider>().isTrigger = true;
        //Debug.Log(enoughResource);
        GetComponent<MeshRenderer>().material = enoughResource ? blueprintMat : blueprintMatGray;
    }
    public void SetAsBuilding()
    {
        isBlueprint = false;
        GetComponent<Collider>().isTrigger = false;
        GetComponent<MeshRenderer>().material = buildingMat;
    }

    private void OnTriggerEnter()
    {
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
            if (collisionCount == 0)
            {
                cannotBuild = false;
                if (enoughResource)
                {
                    GetComponent<MeshRenderer>().material = blueprintMat;
                }
                else
                    GetComponent<MeshRenderer>().material = blueprintMatGray;
            }
        }
    }
}
