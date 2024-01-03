using UnityEngine;

public class GroundFire : Ability
{
    Player player;

    public float attackRadius = 2.5f;
    public int attack = 5;
    public int attackCount = 2;
    CapsuleCollider attackCollider;
    float attackTimer = 0;

    void Start() 
    {
        player = FindObjectOfType<Player>();
    }

    public override void Activate()
    {
        if (attackCollider == null)
        {
            attackCollider = this.gameObject.AddComponent<CapsuleCollider>();
            attackCollider.isTrigger = true;
            attackCollider.radius = attackRadius;
        }
        attackCollider.enabled = true;
    }

    public override void Deactivate() { 
        if (attackCollider != null)
        {
            attackCollider.enabled = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                other.GetComponent<Monster>().TakeDamage(attack);
                attackTimer = activeTime / 4;
            }
        }
    }
}
