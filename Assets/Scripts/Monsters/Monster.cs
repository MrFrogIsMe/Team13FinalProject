<<<<<<< HEAD
using System.Collections.Generic;
using UnityEngine;
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
>>>>>>> test

public class Monster : Entity
{
    public Tower tower;
    public Player player;

<<<<<<< HEAD
    public int dropExp;

=======
>>>>>>> test
    // the target the monster is currently chasing
    GameObject chaseTarget;
    // keep track of a list of targets in the chase range
    LinkedList<GameObject> chaseList = new LinkedList<GameObject>();

    // the target the monster is currently attacking
    GameObject attackTarget;
    // keep track of a list of targets in the attack range
    LinkedList<GameObject> attackList = new LinkedList<GameObject>();
    bool isAttacking = false;

<<<<<<< HEAD
=======
    Animator anim;

>>>>>>> test
    void Start()
    {
        tower = FindObjectOfType<Tower>();
        player = FindObjectOfType<Player>();
        Setup();

<<<<<<< HEAD
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
=======
        maxHp = 20;
        hp = maxHp;
        healthBar.SetMaxHealth(maxHp);

        damage = 2;
        attackCD = 0.5f;
        maxSpeed = 5f;
        force = 1500f;
        drag = 2f;
>>>>>>> test

        chaseTarget = tower.gameObject;

        // Attack() is called every .5 seconds to enhance performance
        InvokeRepeating("Attack", 0f, 0.5f);
<<<<<<< HEAD
=======

        anim = GetComponent<Animator>() ;
>>>>>>> test
    }

    void Update()
    {
        // Check if the monster is alive
<<<<<<< HEAD
        if (health <= 0 && gameObject != null)
        {
            Die();
        }

=======
        if (hp <= 0 && gameObject != null)
        {
            Destroy(gameObject);
        }
        
>>>>>>> test
        Move();
    }

    public override void Move()
    {
        // in case the current chase target has been destroyed
        if (chaseTarget == null)
        {
            chaseTarget = FindNewChaseTarget();
        }

        Vector3 direction = chaseTarget.transform.position - transform.position;
        direction.y = 0f;
        transform.forward = direction;

        rb.velocity = Vector3.zero;

        // the monster cannot move while attacking
        if (!isAttacking)
        {
            rb.AddForce(transform.forward.normalized * force);
<<<<<<< HEAD

=======
            anim.SetTrigger("Jump");
>>>>>>> test
            if (rb.velocity.sqrMagnitude > maxSpeed * maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
<<<<<<< HEAD
=======

>>>>>>> test
    }

    public override void Attack()
    {
        // in case the current attack target has been destroyed
        if (attackTarget == null)
        {
            attackTarget = FindNewAttackTarget();
        }

        // if there are targets in the chase range
        if (attackTarget != null)
        {
<<<<<<< HEAD
            isAttacking = true;
            attackTarget.GetComponent<Entity>().TakeDamage(attack);
=======
            anim.SetTrigger("Attack");
            isAttacking = true;
            attackTarget.GetComponent<Entity>().TakeDamage(damage);
>>>>>>> test
        }
        else
        {
            isAttacking = false;
        }
    }

<<<<<<< HEAD
    public override void Die()
    {
        ExperienceSystem.Instance.AddExperience(dropExp);
        Destroy(gameObject);
    }

=======
>>>>>>> test
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

            if (other.gameObject == chaseTarget)
            {
                chaseTarget = FindNewChaseTarget();
            }
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
<<<<<<< HEAD
                nearestTarget = currentTarget;
=======
                nearestTarget= currentTarget;
>>>>>>> test
            }
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
<<<<<<< HEAD
                nearestTarget = currentTarget;
=======
                nearestTarget= currentTarget;
>>>>>>> test
            }
        }

        return nearestTarget;
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> test
