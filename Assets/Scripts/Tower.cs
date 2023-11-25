public class Tower : Entity
{
    void Start()
    {
        this.Setup();
        maxHp = 200;
        hp = maxHp;
        hpBar.SetMaxHealth(maxHp);
    }

    void Update()
    {
        if (hp <= 0)
        {
            // game over
        }
    }

    public override void Move() { }

    public override void Attack() { }
}
