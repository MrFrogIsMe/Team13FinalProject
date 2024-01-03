using System.Collections.Generic;
using UnityEngine;

public class Monster : Entity
{
    public Tower tower;
    public Player player;

    // the target the monster is currently chasing
    public GameObject chaseTarget;
    // keep track of a list of targets in the chase range
    LinkedList<GameObject> chaseList = new LinkedList<GameObject>();

    // the target the monster is currently attacking
    public GameObject attackTarget;
    // keep track of a list of targets in the attack range
    LinkedList<GameObject> attackList = new LinkedList<GameObject>();
    bool isAttacking = false;

    [HideInInspector]
    public bool isFreezing = false;

    Animator anim;

    public int dropExp;

    void Start()
    {
        tower = FindObjectOfType<Tower>();
        player = FindObjectOfType<Player>();
        Setup();

        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        chaseTarget = tower.gameObject;

        // Attack() is called every .5 seconds to enhance performance
        InvokeRepeating("Attack", 0f, 0.5f);

        anim = GetComponent<Animator>();
    }

    public override void Die()
    {
        ExperienceSystem.Instance.AddExperience(dropExp);
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        // Check if the monster is alive
        if (health <= 0 && gameObject != null)
        {
            Die();
        }

        Move();
    }

    public override void Move()
    {
        // in case the current chase target has been destroyed
        chaseTarget = FindNewChaseTarget();
        if (chaseTarget == null)
        {
            return;
        }

        Vector3 direction = chaseTarget.transform.position - transform.position;
        direction.y = 0f;
        transform.forward = direction;

        rb.velocity = Vector3.zero;

        // the monster cannot move while attacking
        if (!isAttacking && !isFreezing)
        {
            rb.AddForce(transform.forward.normalized * force);
            anim.SetTrigger("Jump");
            if (rb.velocity.sqrMagnitude > maxSpeed * maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
    }

    public override void Attack()
    {
        // in case the current attack target has been destroyed
        if (attackTarget == null)
        {
            isAttacking = false;
            attackTarget = FindNewAttackTarget();
        }

        // if there are targets in the chase range
        else
        {
            anim.SetTrigger("Attack");
            isAttacking = true;
            attackTarget.GetComponent<Entity>().TakeDamage(attack);
        }
    }

    public void OnChaseTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Building") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Tower"))
        {
            if (other.gameObject.CompareTag("Building"))
            {
                Debug.Log("Building entered chase range.");
            }
            else if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player entered chase range.");
            }
            else
            {
                Debug.Log("Tower entered chase range.");
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
        if (other.gameObject.CompareTag("Building") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Tower"))
        {
            if (other.gameObject.CompareTag("Building"))
            {
                Debug.Log("Building left chase range.");
            }
            else if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player left chase range.");
            }
            else
            {
                Debug.Log("Tower left chase range.");
            }

            chaseList.Remove(other.gameObject);
        }
    }

    public void OnAttackTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Building") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Tower"))
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

            attackList.AddLast(other.gameObject);

            if (attackList.Count == 1)
            {
                attackTarget = other.gameObject;
            }
        }

    }

    public void OnAttackTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Building") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Tower"))
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

            attackList.Remove(other.gameObject);

            if (other.gameObject == attackTarget)
            {
                attackTarget = FindNewAttackTarget();
            }
        }
    }

    // returns the nearest building or player, or tower otherwise
    GameObject FindNewChaseTarget()
    {
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
                nearestTarget = currentTarget;
            }

            currentNode = currentNode.Next;
        }

        // return tower if no buildings or player are in the chase range
        if (nearestTarget == null)
        {
            return tower.gameObject;
        }

        return nearestTarget;
    }

    // returns the nearest building or player, or null otherwise
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
                nearestTarget = currentTarget;
            }
            currentNode = currentNode.Next;
        }

        return nearestTarget;
    }
}
