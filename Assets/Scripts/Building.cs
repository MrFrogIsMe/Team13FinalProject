<<<<<<< HEAD
using System.Collections.Generic;
using UnityEngine;
using System;
=======
using UnityEngine;
>>>>>>> test

public class Building : Entity
{
    [Header("Config")]
    [SerializeField] private Material blueprintMat;
    [SerializeField] private Material blueprintMatErr;
<<<<<<< HEAD
    [SerializeField] private Material blueprintMatGray;
    [SerializeField] private Material buildingMat;


    [Header("State")]
    public bool cannotBuild;
    public bool enoughResource;
=======
    [SerializeField] private Material buildingMat;
    

    [Header("State")]
    public bool cannotBuild;
>>>>>>> test
    [SerializeField] private bool isBlueprint;
    [SerializeField] private int collisionCount;

    [Header("Buliding Stat")]
    [SerializeField] float MaxHealth = 100;
<<<<<<< HEAD
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
=======
    [SerializeField] float health = 100;
>>>>>>> test

    void Start()
    {
        healthBar.SetMaxHealth((int)MaxHealth);
<<<<<<< HEAD
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
=======
        collisionCount = 0;  
    }

    public override void Move() { 
    }
    public override void Attack()
    {
    }


    public void setMaxHealth(float hp) { 
        MaxHealth = hp;
        health = hp;
        healthBar.SetMaxHealth((int)hp);
    }

    void setHealth(float hp) { 
        health = hp >= MaxHealth ? MaxHealth : hp;
    }

    public void SetAsBlueprint() { 
        isBlueprint = true;
        GetComponent<Collider>().isTrigger = true;
        GetComponent<MeshRenderer>().material = blueprintMat;
    }
    public void SetAsBuilding(){
>>>>>>> test
        isBlueprint = false;
        GetComponent<Collider>().isTrigger = false;
        GetComponent<MeshRenderer>().material = buildingMat;
    }

<<<<<<< HEAD
    private void OnTriggerEnter()
    {
=======
    private void OnTriggerEnter() {
>>>>>>> test
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
<<<<<<< HEAD
            if (collisionCount == 0)
            {
                cannotBuild = false;
                if (enoughResource)
                {
                    GetComponent<MeshRenderer>().material = blueprintMat;
                }
                else
                    GetComponent<MeshRenderer>().material = blueprintMatGray;
=======
            if (collisionCount == 0){
                cannotBuild = false;
                GetComponent<MeshRenderer>().material = blueprintMat;
>>>>>>> test
            }
        }
    }
}
