using UnityEngine;

public class Building : Entity
{
    [Header("Config")]
    [SerializeField] private Material blueprintMat;
    [SerializeField] private Material blueprintMatErr;
    [SerializeField] private Material buildingMat;
    

    [Header("State")]
    public bool cannotBuild;
    [SerializeField] private bool isBlueprint;
    [SerializeField] private int collisionCount;

    [Header("Buliding Stat")]
    [SerializeField] float MaxHealth = 100;
    [SerializeField] float health = 100;

    void Start()
    {
        healthBar.SetMaxHealth((int)MaxHealth);
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
