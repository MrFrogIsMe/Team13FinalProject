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
    void Start()
    {
        anim = GetComponent<Animator>() ;
        this.Setup();
        hp = maxHp;
        healthBar.SetMaxHealth(maxHp);
        mainCamera = FindObjectOfType<Camera>();
        resources = new Dictionary<String, int>();
        resources.Add("Tree", 0);
        resources.Add("Stone", 0);
    }

    void Update()
    {

        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        this.Move();
        this.TrackMouseRotation();

        if (hp <= 0)
        {
            // game over
            Debug.Log("Player died.");
            anim.SetTrigger("dead");
        }
    }

    public override void Move()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(moveHorizontal, 0f, moveVertical);

        // Move by adding force
        // use sqrMagnitude to increace performance
        rb.AddForce(direction.normalized * force);
        if(direction.normalized * force!=new Vector3(0f, 0f, 0f)){
            anim.SetBool("run",true);
        }else{
            anim.SetBool("run",false);
        }
        
        if (rb.velocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed * Time.deltaTime;
        }
    }

    public override void Attack()
    {
        // Shoot fireball
        fireballTemplate.transform.position = fireballSpawnpoint.transform.position;
        fireballTemplate.transform.forward = this.transform.forward;
        fireballTemplate.damage = this.damage;
        Instantiate(fireballTemplate);
        anim.SetTrigger("attack");
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
}
