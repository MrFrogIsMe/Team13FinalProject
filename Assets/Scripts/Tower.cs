public class Tower : Entity
{
    void Start()
    {
        this.SetStatus(200f, 0f, 0f, 0f);
    }

    void Update()
    {
        ;
    }

    public override void Move() { }

    public override void Attack() { }
}
