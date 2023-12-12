using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Building
{
    public float rotationSpeed = 5.0f;

    // the target the cannon is currently attacking
    GameObject attackTarget;
    // keep track of a list of targets in the attack range
    LinkedList<GameObject> attackList = new LinkedList<GameObject>();

    void Start()
    {
        // hp = 100
        setMaxHealth(100);

        damage = 10;
        attackCD = 0.5f;

        // Attack() is called every .5 seconds to enhance performance
        InvokeRepeating("Attack", 0f, 0.5f);
    }

    void Update()
    {
        // Check if the monster is alive
        if (hp <= 0 && gameObject != null)
        {
            Destroy(gameObject);
        }
    }

    public override void Move()
    {
        ;
    }

    public override void Attack()
    {
        // in case the current attack target has been destroyed
        if (attackTarget == null)
        {
            attackTarget = FindNewAttackTarget();
        }

        if (attackTarget != null)
        {
            // rotate towards the target before attacking
            RotateTowardsTarget(attackTarget);

            attackTarget.GetComponent<Entity>().TakeDamage(damage);
        }
    }

    void RotateTowardsTarget(GameObject target)
    {
        Vector3 direction = target.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation,
        targetRotation, Time.deltaTime * rotationSpeed);
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            attackList.AddLast(other.gameObject);

            if(attackList.Count == 1)
            {
                attackTarget = other.gameObject;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            attackList.Remove(other.gameObject);

            if(other.gameObject == attackTarget)
            {
                attackTarget = FindNewAttackTarget();
            }
        }
    }

    GameObject FindNewAttackTarget()
    {
        GameObject nearestTarget = null;
        float nearestDistance = float.MaxValue;

        // iterate through the targets within the range and find the nearest
        LinkedListNode<GameObject> currentNode = attackList.First;

        while (currentNode != null)
        {
            GameObject currentTarget = currentNode.Value;

            // remove the current node if this target has been destroyed
            if (currentTarget == null)
            {
                LinkedListNode<GameObject> nextNode = currentNode.Next;
                attackList.Remove(currentNode);
                currentNode = nextNode;
                continue;
            }

            float distance = Vector3.Distance(transform.position, currentTarget.gameObject.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestTarget= currentTarget;
            }
        }

        return nearestTarget;
    }
}
