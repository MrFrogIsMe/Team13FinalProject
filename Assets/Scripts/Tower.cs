using UnityEngine;

public class Tower : Entity
{
    void Start()
    {
        this.SetStatus(200, 0, 0);
    }

    void Update()
    {
        ;
    }

    public override void Move() { }

    public override void Attack() { }

    public override void TakeDamage(float damage) { }
}
