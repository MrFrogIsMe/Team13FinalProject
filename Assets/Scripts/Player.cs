using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public Dictionary<String, int> resources;
    public GameObject fireballSpawnpoint;
    public FireBall fireballTemplate;
    
    Camera mainCamera;
    Animator anim;
    public Inventory inv;

    void Start()
    {
        anim = GetComponent<Animator>() ;
        this.Setup();
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        mainCamera = FindObjectOfType<Camera>();
        resources = new Dictionary<String, int>();
        resources.Add("Tree", 0);
        resources.Add("Stone", 0);
    }

    void Update()
    {
        this.Move();
        this.TrackMouseRotation();

        if (health <= 0)
        {
            this.Die();
        }

    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(moveHorizontal, 0f, moveVertical);
        rb.AddForce(direction.normalized * maxSpeed *  5f);
    }

    public override void Move()
    {
        if(rb.velocity != Vector3.zero){
            anim.SetBool("run",true);
        }else{
            anim.SetBool("run",false);
        }
        
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (flatVel.magnitude >= maxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }

        float groundCheckDistance = GetComponent<BoxCollider>().size.y / 2 + 0.2f;
        bool grounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, groundCheckDistance);
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    public override void Attack()
    {
        // Shoot fireball
        fireballTemplate.transform.position = fireballSpawnpoint.transform.position;
        fireballTemplate.transform.forward = this.transform.forward;
        fireballTemplate.damage = this.attack;
        Instantiate(fireballTemplate);
        anim.SetTrigger("attack");
    }

    public override void TakeDamage(int damage)
    {
        health = health - damage < 0 ? 0 : health - damage;
        healthBar.SetHealth(health);
    }

    void TrackMouseRotation()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (ground.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
            this.transform.LookAt(new Vector3(pointToLook.x, this.transform.position.y, pointToLook.z));
        }
    }

    public override void Die()
    {
        // game over
        Debug.Log("GameOver : Player Died");
    }

    /* Experience System */
    void OnEnable()
    {
        ExperienceSystem.Instance.OnLevelChange.AddListener(LevelUp);
    }

    void OnDisable()
    {
        ExperienceSystem.Instance.OnLevelChange.RemoveListener(LevelUp);
    }

    void LevelUp()
    {
        attack += 5;
        health += 5;
    }
}
