using UnityEngine;

public class GroundFire : Ability
{
    public float attackRadius = 2.5f;
    public int attack = 5;
    CapsuleCollider attackCollider;
    float attackTimer = 0;

    public override void Activate()
    {
        if (attackCollider == null)
        {
            attackCollider = this.gameObject.AddComponent<CapsuleCollider>();
            attackCollider.isTrigger = true;
            attackCollider.radius = attackRadius;
        }
        attackCollider.enabled = true;
        Debug.Log("GroundFire!");
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
