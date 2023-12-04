using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Monster : Entity
{
    public Tower tower;
    public Player player;

    // the target the monster is currently chasing
    GameObject chaseTarget;
    // a list of targets in the attack range
    LinkedList<GameObject> chaseList = new LinkedList<GameObject>();

    GameObject attackTarget;
    // a list of targets to be attacked (Monsters attack in FIFO manner)
    LinkedList<Collider> attackTargets = new LinkedList<Collider>();

    void Start()
    {
        tower = FindObjectOfType<Tower>();
        player = FindObjectOfType<Player>();
        Setup();

        maxHp = 20;
        hp = maxHp;
        healthBar.SetMaxHealth(maxHp);

        damage = 2;
        attackCD = 0.5f;
        maxSpeed = 5f;
        force = 200f;
        drag = 2f;

        chaseTarget = tower.gameObject;

        Attack();
    }

    void Update()
    {
        // Check if the monster is alive
        if (hp <= 0 && gameObject != null)
        {
            Destroy(gameObject);
        }

        Move();
    }

    public override void Move()
    {
        // In case the current chaseTarget has been destroyed
        if (chaseTarget == null)
        {
            chaseTarget = FindNewChaseTarget();
        }

        Vector3 direction = chaseTarget.transform.position - transform.position;
        direction.y = 0f;
        transform.forward = direction;

        rb.velocity = Vector3.zero;

        rb.AddForce(direction.normalized * force);
        
        if (rb.velocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    public override void Attack()
    {
        // calls DealDamageToTarget() every .5 seconds after 0 seconds
        InvokeRepeating("DealDamageToTarget", 0f, 0.5f);
    }

    void DealDamageToTarget()
    {
        if (attackTargets.Count > 0)
        {
            // Peek next
            Collider target = attackTargets.First.Value;

            // Check if the target is null or destroyed
            if (target == null || target.gameObject == null || !target.gameObject.activeInHierarchy)
            {
                attackTargets.RemoveFirst();
                // Recursive call
                DealDamageToTarget();
                return;
            }

            target.gameObject.GetComponent<Entity>().TakeDamage(damage);
            attackTargets.RemoveFirst();
        }
    }
    public void OnChaseTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Building") || other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.CompareTag("Building"))
            {
                Debug.Log("Building entered chase range.");
            }
            else if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player entered chase range.");
            }

            chaseList.AddLast(other.gameObject);

            if (chaseList.Count == 1)
            {
                chaseTarget = other.gameObject;
            }
        }
    }

    public void OnChaseTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Building") || other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.CompareTag("Building"))
            {
                Debug.Log("Building left chase range.");
            }
            else if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player left chase range.");
            }

            chaseList.Remove(other.gameObject);

            if (other.gameObject == chaseTarget)
            {
                chaseTarget = FindNewChaseTarget();
            }
        }
    }

    public void OnAttackTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Building"))
        {
            Debug.Log("Building entered attack range.");

        }
        else if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered attack range.");
        }
        else if (other.gameObject.CompareTag("Tower"))
        {
            Debug.Log("Tower entered attack range.");
        }

        maxSpeed = 0f;
    }

    public void OnAttackTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Building"))
        {
            Debug.Log("Building left attack range.");
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player left attack range.");
        }
        else if (other.gameObject.CompareTag("Tower"))
        {
            Debug.Log("Tower left attack range.");
        }

        maxSpeed = 5f;
    }

    // returns the nearest building or player, or tower otherwise
    GameObject FindNewChaseTarget()
    {
        // the nearestTarget is the nearest building or player, or null otherwise
        GameObject nearestTarget = null;
        float nearestDistance = float.MaxValue;

        // iterate through the targets within the range and find the nearest
        LinkedListNode<GameObject> currentNode = chaseList.First;

        while (currentNode != null)
        {
            GameObject currentTarget = currentNode.Value;

            // remove the current node if this target has been destroyed
            if (currentTarget == null)
            {
                LinkedListNode<GameObject> nextNode = currentNode.Next;
                chaseList.Remove(currentNode);
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

        // return tower if no buildings or player are in the chase range
        if (nearestTarget == null)
        {
            return tower.gameObject;
        }

        return nearestTarget;
    }
}