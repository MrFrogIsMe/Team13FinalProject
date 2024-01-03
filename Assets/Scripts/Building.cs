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
    [SerializeField] private float ground;

    [Header("State")]
    public bool cannotBuild;
    public bool enoughResource;
    [SerializeField] private bool isBlueprint;
    [SerializeField] private int collisionCount;

    [Header("Recipe")]
    [SerializeField] List<String> resources;
    [SerializeField] List<int> quantity;

    public Dictionary<String, int> getRecipe()
    {
        Dictionary<String, int> rep = new Dictionary<String, int>();
        for (int i = 0; i < resources.Count; i++)
        {
            rep.Add(resources[i], quantity[i]);
        }
        return rep;
    }

    public float getGroundCoor()
    {
        return ground;
    }

    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        collisionCount = 0;

        int r = resources.Count;
        int q = quantity.Count;
        if (r != q)
        {
            if (r > q)
            {
                for (int i = q - 1; i < r; i++)
                {
                    quantity.Add(0);
                }
            }
            else
            {
                for (int i = r - 1; i < q; i++)
                {
                    resources.Add("Resource");
                }
            }
        }
    }

    void Update()
    {
        print(health);
        if (health <= 0)
        {
            Die();
        }
    }

    public override void Move() { }
    public override void Attack() { }

    public override void TakeDamage(int damage)
    {
        health -= damage;
        health = (health > 0) ? health : 0;
        healthBar.SetHealth(health);
    }

    public override void Die()
    {
        Destroy(this.gameObject);
    }


    public void setMaxHealth(int hp)
    {
        maxHealth = hp;
        health = hp;
        healthBar.SetMaxHealth(hp);
    }

    void setHealth(int hp)
    {
        health = hp >= maxHealth ? maxHealth : hp;
    }

    public void SetAsBlueprint()
    {
        isBlueprint = true;
        this.gameObject.layer = LayerMask.NameToLayer("Blueprint");
        GetComponent<Collider>().isTrigger = true;
        //Debug.Log(enoughResource);
        GetComponent<MeshRenderer>().material = enoughResource ? blueprintMat : blueprintMatGray;
    }
    public void SetAsBuilding()
    {
        isBlueprint = false;
        this.gameObject.layer = LayerMask.NameToLayer("Building");
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
