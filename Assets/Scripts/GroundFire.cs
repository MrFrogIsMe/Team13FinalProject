using UnityEngine;

public class GroundFire : Ability
{
    public float attackRadius = 2.5f;
    public int attack = 5;
    CapsuleCollider attackCollider;
    float attackTimer = 0;

    public GameObject ExplosionPrefab;
    public GameObject GroundFirePrefab;

    public Transform player;

    private GameObject GroundF;

    public override void Activate()
    {
        if (attackCollider == null)
        {
            attackCollider = this.gameObject.AddComponent<CapsuleCollider>();
            attackCollider.isTrigger = true;
            attackCollider.radius = attackRadius;
            
        }
        attackCollider.enabled = true;
        Quaternion rotation = Quaternion.Euler(-90, 90, 0);
        GroundF = Instantiate(GroundFirePrefab, this.transform.position,rotation);
        GroundF.transform.parent= player;
        Debug.Log("GroundFire!");
    }

    public override void Deactivate() { 
        if (attackCollider != null)
        {
            Destroy(GroundF);
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
                Instantiate(ExplosionPrefab, other.transform.position,this.transform.rotation);
                attackTimer = activeTime / 4;
            }
        }
    }
}
